namespace FastDBEngineProfilerLib
{
    using System;

    [Serializable]
    public enum InfoType
    {
        OpenConnection,
        StartExecuteSP,
        StartExecuteSQL,
        ExecuteFinished,
        ExecuteFailed
    }
}

