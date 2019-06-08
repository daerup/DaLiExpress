using System.Collections.Generic;
using System.Data.Entity;
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


        public IEnumerable<Publisher> GetBestRatedPublishers()
        {
            Publisher publisher = this.daliGameExpressEntities.Publisher.OrderBy(p => this.GetAverageGameRating(p))
                .First();
            int? highestAverageGameRating = this.GetAverageGameRating(publisher);
            return this.daliGameExpressEntities.Publisher.Where(p =>
                this.GetAverageGameRating(p) >= highestAverageGameRating);
        }

        private int? GetAverageGameRating(Publisher publisher)
        {
            return publisher.Game.Sum(g => g.Rating) / publisher.Game.Count;
        }
    }
}