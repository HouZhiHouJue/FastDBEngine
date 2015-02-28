namespace FastDBEngine
{
    using System;

    public abstract class DbContextHolderBase : IDbContextHolder
    {
        private bool Isdispose;
        private FastDBEngine.DbContext dbContext;

        protected DbContextHolderBase()
        {
        }

        public virtual FastDBEngine.DbContext CreateDbContext(bool supportTranscation)
        {
            return new FastDBEngine.DbContext(supportTranscation);
        }

        public T CreateHolder<T>() where T: class, IDbContextHolder, new()
        {
            T local = Activator.CreateInstance(typeof(T)) as T;
            local.DbContext = this.DbContext;
            return local;
        }

        public void Dispose()
        {
            if ((this.dbContext != null) && this.Isdispose)
            {
                this.dbContext.Dispose();
                this.dbContext = null;
                this.Isdispose = false;
            }
        }

        public FastDBEngine.DbContext DbContext
        {
            get
            {
                if (this.dbContext == null)
                {
                    this.dbContext = this.CreateDbContext(false);
                    this.Isdispose = true;
                }
                return this.dbContext;
            }
            set
            {
                this.Dispose();
                this.dbContext = value;
            }
        }
    }
}

