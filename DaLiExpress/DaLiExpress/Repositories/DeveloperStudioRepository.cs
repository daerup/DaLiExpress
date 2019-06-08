using System.Data.Entity;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public class DeveloperStudioRepository : RepositoryBase<DeveloperStudio>, IDeveloperStudioRepository
    {
        public DeveloperStudioRepository(DbContext context) : base(context)
        {
        }
    }
}