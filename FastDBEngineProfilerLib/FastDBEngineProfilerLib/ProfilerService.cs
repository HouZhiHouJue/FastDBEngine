namespace FastDBEngineProfilerLib
{
    using System;
    using System.Runtime.CompilerServices;

    public class ProfilerService : MarshalByRefObject
    {
        public static  event MessageReceive OnMessageReceive;

        public void EmptyMethod()
        {
        }

        public void SendMessage(ExecuteInfo info)
        {
            MessageReceive onMessageReceive = OnMessageReceive;
            if (onMessageReceive != null)
            {
                onMessageReceive(info);
            }
        }
    }
}

