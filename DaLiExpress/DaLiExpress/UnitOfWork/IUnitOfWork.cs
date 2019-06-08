using System;
using DaLiExpress.Repositories;

namespace DaLiExpress.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Game { get; }

        int Complete();
    }
}