using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        private readonly DaLi_GameExpressEntities daliGameExpressEntities;
        public GameRepository(DaLi_GameExpressEntities context) : base(context)
        {
            this.daliGameExpressEntities = context;
        }

        public IEnumerable<Game> GetHighestRatedGames(int numberOfGamesToReceive)
        {
            return this.daliGameExpressEntities.Game.OrderBy(g => g.Rating).Take(numberOfGamesToReceive).ToList();
        }

        public Game GetRandomGame()
        {
            return this.daliGameExpressEntities.Game.ToList().ElementAt(new Random().Next(this.daliGameExpressEntities.Game.Count()));
        }
    }
}