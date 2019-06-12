using System.Collections.Generic;
using System.Linq;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public class DeveloperStudioRepository : RepositoryBase<DeveloperStudio>, IDeveloperStudioRepository
    {
        private readonly DaLi_GameExpressEntities daliGameExpressEntities;

        public DeveloperStudioRepository(DaLi_GameExpressEntities context) : base(context)
        {
            this.daliGameExpressEntities = context;
        }

        public List<DeveloperStudio> GetMostProductiveDeveloperStudios()
        {
            if (!this.daliGameExpressEntities.DeveloperStudio.Any())
            {
                return new List<DeveloperStudio>();
            }

            int mostGamesProduced = this.daliGameExpressEntities.DeveloperStudio.ToList().OrderByDescending(p => p.Game.Count).First().Game.Count;
            return this.daliGameExpressEntities.DeveloperStudio.Where(p => p.Game.Count.Equals(mostGamesProduced)).ToList();
        }

        public List<DeveloperStudio> GetLeastProductiveDeveloperStudios()
        {
            if (!this.daliGameExpressEntities.DeveloperStudio.Any())
            {
                return new List<DeveloperStudio>();
            }

            int mostGamesProduced = this.daliGameExpressEntities.DeveloperStudio.ToList().OrderBy(p => p.Game.Count).First().Game.Count;
            return this.daliGameExpressEntities.DeveloperStudio.Where(p => p.Game.Count.Equals(mostGamesProduced)).ToList();
        }
    }
}