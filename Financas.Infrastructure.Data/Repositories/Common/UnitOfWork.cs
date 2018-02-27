using System;
using Financas.Domain.Contracts.Repositories.Common;
using Financas.Infrastructure.Data.Contracts;

namespace Financas.Infrastructure.Data.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext context;
        private bool disposed;

        public UnitOfWork(IDbContext context)
        {
            this.context = context;
        }

        public void BeginTransaction()
        {
            disposed = false;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
