using System.Collections.Generic;
using System.Linq;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public class PublisherRepository : RepositoryBase<Publisher>, IPublisherRepository
    {
        private readonly DaLi_GameExpressEntities daliGameExpressEntities;

        public PublisherRepository(DaLi_GameExpressEntities context) : base(context)
        {
            this.daliGameExpressEntities = context;
        }

        public List<Publisher> GetBestRatedPublishers()
        {
            if (!this.daliGameExpressEntities.Publisher.Any())
            {
                return new List<Publisher>();
            }

            Publisher publisher = this.daliGameExpressEntities.Publisher.ToList().OrderByDescending(this.GetAverageGameRating).First();
            int? highestAverageGameRating = this.GetAverageGameRating(publisher);
            return this.daliGameExpressEntities.Publisher.ToList().Where(p => this.GetAverageGameRating(p) >= highestAverageGameRating).ToList();
        }

        private int? GetAverageGameRating(Publisher publisher)
        {
            if (publisher.Game == null || publisher.Game.Count == 0)
            {
                return 0;
            }

            return publisher.Game.Sum(g => g.Rating) / publisher.Game.Count;
        }
    }
}