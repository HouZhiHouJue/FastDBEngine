namespace FastDBEngine
{
    using System;
    using System.Runtime.CompilerServices;

    public static class DbContextDefaultSetting
    {
        private static int listResultCapacity = 50;

        public static bool AutoRetrieveOutputValues { get; set; }

        public static int ListResultCapacity
        {
            get
            {
                return listResultCapacity;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("ListResultCapacity must more than zero.");
                }
                listResultCapacity = value;
            }
        }
    }
}

