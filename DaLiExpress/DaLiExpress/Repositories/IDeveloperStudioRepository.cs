using System.Collections.Generic;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public interface IDeveloperStudioRepository : IRepositoryBase<DeveloperStudio>
    {
        List<DeveloperStudio> GetMostProductiveDeveloperStudios();
        List<DeveloperStudio> GetLeastProductiveDeveloperStudios();
    }
}