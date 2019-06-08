using System.Collections.Generic;
using System.Linq;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public class PlatformRepository : RepositoryBase<Platform>, IPlatformRepository
    {
        private readonly DaLi_GameExpressEntities daliGameExpressEntities;
        public PlatformRepository(DaLi_GameExpressEntities context) : base(context)
        {
            this.daliGameExpressEntities = context;
        }

        public IEnumerable<Platform> GetMostPopularPlatforms()
        {
            int mostPopular = this.daliGameExpressEntities.Platform.OrderBy(p => p.Game.Count).First().Game.Count;
            return this.daliGameExpressEntities.Platform.Where(p => p.Game.Count.Equals(mostPopular));
        }

        public IEnumerable<Platform> GetLeastPopularPlatforms()
        {
            int leastPopular = this.daliGameExpressEntities.Platform.OrderByDescending(p => p.Game.Count).First().Game.Count;
            return this.daliGameExpressEntities.Platform.Where(p => p.Game.Count.Equals(leastPopular));
        }
    }
}