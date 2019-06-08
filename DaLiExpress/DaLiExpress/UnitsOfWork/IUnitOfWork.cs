using System;
using DaLiExpress.Repositories;

namespace DaLiExpress.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Game { get; }

        int Complete();
    }
}