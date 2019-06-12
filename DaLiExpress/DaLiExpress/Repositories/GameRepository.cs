using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Game> GetHighestRatedGames()
        {
            if (!this.daliGameExpressEntities.Game.Any())
            {
                return new List<Game>();
            }

            int? highestRating = this.daliGameExpressEntities.Game.ToList().OrderByDescending(p => p.Rating).First().Rating;
            return this.daliGameExpressEntities.Game.ToList().Where(p => p.Rating.Equals(highestRating)).ToList().ToList();
        }

        public Game GetRandomGame()
        {
            return !this.daliGameExpressEntities.Game.Any() ? null : this.daliGameExpressEntities.Game.ToList().ElementAt(new Random().Next(this.daliGameExpressEntities.Game.Count()));
        }
    }
}