﻿namespace FastDBEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static class BuildManager
    {
        private static Dictionary<Type, int> dictionary = new Dictionary<Type, int>(0x800);
        private static Func<bool> funcCompileCondition = null;
        private static int requestCount = 0;
        internal static int int1 = 0;
        private static readonly object objLock = new object();
        private static Timer timer = null;

        public static event BuildExceptionHandler OnBuildException;
        private static BuildExceptionHandler buildExceptionHandler;


        public static void CompileModelTypesAsync(Type[] types)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(BuildManager.CompileModelTypesSyncWrapper), types);
        }

        public static void CompileModelTypesSync(Type[] types, bool throwOnFailure)
        {
            if (throwOnFailure)
            {
                CompileDelegates(types);
            }
            else
            {
                try
                {
                    CompileDelegates(types);
                }
                catch (Exception exception)
                {
                    BuildExceptionHandler handler = buildExceptionHandler;
                    if (handler != null)
                    {
                        try
                        {
                            handler(exception);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public static Type[] FindModelTypes(Func<Type, bool> predicate, params Assembly[] assemblies)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            if ((assemblies == null) || (assemblies.Length == 0))
            {
                throw new ArgumentNullException("array");
            }
            return Enumerable.Select(Enumerable.Where(Enumerable.SelectMany(assemblies, t => { return t.GetExportedTypes(); },
                (a, t) => { return new TypeContainer<Assembly, Type>(a, t); }), new Func<TypeContainer<Assembly, Type>, bool>(
                    typeContainer =>
                    {
                        return (((typeContainer.Ttype.IsValueType || typeContainer.Ttype.IsInterface) || (typeContainer.Ttype.IsGenericType || typeContainer.Ttype.IsAbstract)) ? false :
                            predicate(typeContainer.Ttype));
                    })),
                t => { return t.Ttype; }).ToArray<Type>();
        }

        public static Type[] FindModelTypesFromCurrentApplication(Func<Type, bool> predicate)
        {
            Assembly[] assemblies = AssemblyHelper.GetAppAssemblys();
            return FindModelTypes(predicate, assemblies);
        }

        internal static void EnqueueWaitForCompiledType(Type type)
        {
            if (((!type.IsValueType && !type.IsGenericType) && !type.IsInterface) && !type.IsAbstract)
            {
                lock (objLock)
                {
                    dictionary[type] = 0;
                    requestCount++;
                }
            }
        }

        private static void CheckCompiledTypes(object obj)
        {
            if (funcCompileCondition() && (requestCount != 0))
            {
                Type[] types = null;
                lock (objLock)
                {
                    types = Enumerable.Select<Type, Type>(dictionary.Keys, t => { return t; }).ToArray<Type>();
                    dictionary.Clear();
                    requestCount = 0;
                }
                if ((types != null) && (types.Length != 0))
                {
                    CompileModelTypesAsync(types);
                }
            }
        }

        private static void CompileModelTypesSyncWrapper(object object_1)
        {
            Type[] types = (Type[])object_1;
            CompileModelTypesSync(types, false);
        }

        private static void CompileDelegates(Type[] typeArray)
        {
            if ((typeArray != null) && (typeArray.Length != 0))
            {
                List<Type> list = new List<Type>(typeArray.Length);
                foreach (Type type in typeArray)
                {
                    if (type.GetMeberOperationHelperContainer(false).IsOrigin(EnumState.const_0, EnumState.const_1))
                    {
                        list.Add(type);
                    }
                }
                if (list.Count != 0)
                {
                    List<GenerateCodeInfo> list2 = GenerateCodeHelper.GetGenerateCodeInfoList(list);
                    Assembly assembly = CompileAssemblyHelper.CompileAssembly(list2);
                    using (List<Type>.Enumerator enumerator = list.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            MeberOperationHelperContainer meberOperationHelperContainer = enumerator.Current.GetMeberOperationHelperContainer(false);
                            GenerateCodeInfo generateCodeInfo = Enumerable.First<GenerateCodeInfo>(list2, t => { return t.GenerateParameters.ModelType == enumerator.Current; });
                            Type type2 = assembly.GetType(GenerateCodeHelper.string_0 + "." + generateCodeInfo.GenerateParameters.ClassName);
                            MethodInfo method = type2.GetMethod(generateCodeInfo.GenerateParameters.GenerateModelMethod);
                            MethodInfo info2 = type2.GetMethod(generateCodeInfo.GenerateParameters.GetModelPropertyValueMethod);
                            MethodInfo info3 = type2.GetMethod(generateCodeInfo.GenerateParameters.SetModelPropertyValueByChangeTypeMethod);
                            GenerateModelDelegate delegate2 = (GenerateModelDelegate)Delegate.CreateDelegate(typeof(GenerateModelDelegate), null, method, true);
                            GetPropertyValueByNameDelegate delegate3 = (GetPropertyValueByNameDelegate)Delegate.CreateDelegate(typeof(GetPropertyValueByNameDelegate), null, info2, true);
                            SetPropertyValueByNameDelegate delegate4 = (SetPropertyValueByNameDelegate)Delegate.CreateDelegate(typeof(SetPropertyValueByNameDelegate), null, info3, true);
                            meberOperationHelperContainer.SetDelegates(delegate2, delegate3, delegate4);
                        }
                    }
                }
            }
        }



        public static void StartAutoCompile(Func<bool> func)
        {
            StartAutoCompile(func, 0x1388);
        }

        public static void StartAutoCompile(Func<bool> func, int timerPeriod)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }
            if (timerPeriod < 0xbb8)
            {
                throw new ArgumentOutOfRangeException("timerPeriod", "timerPeriod must more than 3000");
            }
            if (Interlocked.CompareExchange(ref int1, 1, 0) == 1)
            {
                throw new InvalidOperationException("已经开启过自动编译模式。");
            }
            funcCompileCondition = func;
            timer = new Timer(new TimerCallback(BuildManager.CheckCompiledTypes), null, timerPeriod, timerPeriod);
        }

        public static int RequestCount
        {
            get
            {
                return requestCount;
            }
        }

        public static int WaitTypesCount
        {
            get
            {
                lock (objLock)
                {
                    return dictionary.Count;
                }
            }
        }
    }
}

