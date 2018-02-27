using System;

namespace Financas.Domain.Contracts.Repositories.Common
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();

    }
}
