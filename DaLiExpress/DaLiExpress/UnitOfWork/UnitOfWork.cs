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
        }

        public IGameRepository Game { get; }

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