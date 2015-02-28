namespace FastDBEngine
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void DbContextExceptionHandler(DbContext context, Exception exception);
}

