using System;

namespace Order.Domain.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
