using System.Collections.Generic;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public interface IPublisherRepository : IRepositoryBase<Publisher>
    {
        IEnumerable<Publisher> GetBestRatedPublishers();
    }
}