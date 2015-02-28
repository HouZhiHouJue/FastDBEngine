namespace FastDBEngine
{
    using System;

    public interface IDbContextHolder : IDisposable
    {
        FastDBEngine.DbContext DbContext { get; set; }
    }
}

