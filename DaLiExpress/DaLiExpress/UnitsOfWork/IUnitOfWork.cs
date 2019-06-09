using System;
using DaLiExpress.Repositories;

namespace DaLiExpress.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Game { get; }
        IPlatformRepository Platform { get; }
        IDeveloperStudioRepository DeveloperStudio { get; }
        IPublisherRepository Publisher { get; }

        int Complete();
    }
}