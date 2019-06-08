using System.Collections.Generic;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public interface IPlatformRepository : IRepositoryBase<Platform>
    {
        IEnumerable<Platform> GetMostPopularPlatforms();
        IEnumerable<Platform> GetLeastPopularPlatforms();
    }
}