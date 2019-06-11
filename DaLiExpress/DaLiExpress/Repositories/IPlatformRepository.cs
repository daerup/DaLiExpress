using System.Collections.Generic;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public interface IPlatformRepository : IRepositoryBase<Platform>
    {
        List<Platform> GetMostPopularPlatforms();
        List<Platform> GetLeastPopularPlatforms();
    }
}