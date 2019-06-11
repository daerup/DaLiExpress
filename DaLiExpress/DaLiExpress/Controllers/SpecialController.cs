using System.Linq;
using System.Web.Mvc;
using DaLiExpress.Models;
using DaLiExpress.UnitsOfWork;

namespace DaLiExpress.Controllers
{
    public class SpecialController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new DaLi_GameExpressEntities());

        public ActionResult Index()
        {
            this.ViewBag.BestRatedGames = this.unitOfWork.Game.GetHighestRatedGames().Select(g=>g.Name).ToList();
            this.ViewBag.BestRatedPublishers = this.unitOfWork.Publisher.GetBestRatedPublishers().Select(p => p.Name).ToList();
            this.ViewBag.MostProductiveStudio = this.unitOfWork.DeveloperStudio.GetMostProductiveDeveloperStudios().Select(ds => ds.Name).ToList();
            this.ViewBag.LeastProductiveStudio = this.unitOfWork.DeveloperStudio.GetLeastProductiveDeveloperStudios().Select(ds => ds.Name).ToList();
            this.ViewBag.MostPopularPlatforms = this.unitOfWork.Platform.GetMostPopularPlatforms().Select(p => p.Name).ToList();
            this.ViewBag.LeastPopularPlatforms = this.unitOfWork.Platform.GetLeastPopularPlatforms().Select(p => p.Name).ToList();
            //this.ViewBag. = this.unitOfWork.Game.GetRandomGame();
            return this.View();
        }
    }
}