using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public class GamesRepository : RepositoryBase<Game>, IGamesRepository
    {
        public GamesRepository(DaLi_GameExpressEntities context) : base(context)
        {
        }

        public IEnumerable<Game> GetHighestRatedGames(int numberOfGamesToReceive)
        {
            return base.GetAll().OrderBy(g => g.Rating).Take(numberOfGamesToReceive).ToList();
        }

        public Game GetRandomGame()
        {
            throw new System.NotImplementedException();
        }
    }
}