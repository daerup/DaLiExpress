using DaLiExpress.Models;
using DaLiExpress.Repositories;

namespace DaLiExpress.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DaLi_GameExpressEntities context { get; }

        public UnitOfWork(DaLi_GameExpressEntities context)
        {
            this.context = context;
            this.Game = new GameRepository(context);
            this.Platform = new PlatformRepository(context);
            this.DeveloperStudio = new DeveloperStudioRepository(context);
            this.Publisher = new PublisherRepository(context);
        }

        public IGameRepository Game { get; }
        public IPlatformRepository Platform { get; }
        public IDeveloperStudioRepository DeveloperStudio { get; }
        public IPublisherRepository Publisher{ get; }

        public int Complete()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}