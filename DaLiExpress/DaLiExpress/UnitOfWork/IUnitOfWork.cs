using System;
using DaLiExpress.Repositories;

namespace DaLiExpress.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGamesRepository Games { get; }

        int Complete();
    }
}