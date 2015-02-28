namespace FastDBEngineProfilerLib
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Remoting;
    using System.Threading;

    public static class ClientHelper
    {
        private static Queue<ExecuteInfo> s_executeInfoQueue = new Queue<ExecuteInfo>(100);
        private static readonly object s_lock = new object();
        private static int s_retryTime = 0;

        public static ProfilerService GetSQLProfilerService()
        {
            return (ProfilerService) Activator.GetObject(typeof(ProfilerService), MyConst.ServiceURL);
        }

        public static void SendExecuteInfo(ExecuteInfo info)
        {
            lock (s_lock)
            {
                s_executeInfoQueue.Enqueue(info);
            }
        }

        private static void SendMessageToServer()
        {
            ProfilerService ProfilerService = GetSQLProfilerService();
            while (true)
            {
                object obj2;
                ExecuteInfo info = null;
                lock ((obj2 = s_lock))
                {
                    if (s_executeInfoQueue.Count > 0)
                    {
                        info = s_executeInfoQueue.Peek();
                    }
                }
                if (info == null)
                {
                    Thread.Sleep(0x3e8);
                }
                else
                {
                    ProfilerService.SendMessage(info);
                    s_retryTime = 0;
                    lock ((obj2 = s_lock))
                    {
                        s_executeInfoQueue.Dequeue();
                    }
                }
            }
        }

        public static void StartClientThread()
        {
            new Thread(new ThreadStart(ClientHelper.StartClientThread_DoWork)) { IsBackground = true }.Start();
        }

        private static void StartClientThread_DoWork()
        {
            while (true)
            {
                try
                {
                    SendMessageToServer();
                }
                catch (Exception exception)
                {
                    if (exception is RemotingException)
                    {
                        s_retryTime++;
                        if (s_retryTime > 2)
                        {
                            s_retryTime = 0;
                            lock (s_lock)
                            {
                                s_executeInfoQueue.Clear();
                            }
                        }
                    }
                }
            }
        }
    }
}

