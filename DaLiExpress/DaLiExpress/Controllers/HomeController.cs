using System;
using System.Web.Mvc;
using DaLiExpress.Models;
using DaLiExpress.UnitsOfWork;

namespace DaLiExpress.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork(new DaLi_GameExpressEntities());
        public ActionResult Index()
        {
            this.unitOfWork.Game.Add(new Game()
            {
                Name = "Cuphead",
                Release = new DateTime(2013, 10, 23),
                Rating = 83,
                PublisherID = 1,
            });

            var enumerable = this.unitOfWork.Game.GetAll();
            var randomGame = this.unitOfWork.Game.GetRandomGame();
            var complete = this.unitOfWork.Complete();
            return this.View();
        }
        public ActionResult Game()
        {
            this.ViewBag.Message = "Your contact page.";
            return this.View();
        }
        
        
    }
}