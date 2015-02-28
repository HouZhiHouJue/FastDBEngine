namespace FastDBEngineProfilerLib
{
    using System;

    public static class MyConst
    {
        public static readonly string ChannelPortName = "f39c1034fe39e4626acee4fc2f5965596";
        public static readonly string ObjectURI = "FastDBEngineProfilerService.rem";
        public static readonly string ServiceURL = ("ipc://" + ChannelPortName + "/" + ObjectURI);
    }
}

