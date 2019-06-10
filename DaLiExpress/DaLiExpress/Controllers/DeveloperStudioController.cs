using System.Web.Mvc;
using DaLiExpress.Models;
using DaLiExpress.UnitsOfWork;

namespace DaLiExpress.Controllers
{
    public class DeveloperStudioController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork(new DaLi_GameExpressEntities());

        public ActionResult Index()
        {
            this.ViewBag.AllDeveloperStudios = this.unitOfWork.DeveloperStudio.GetAll();
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            return this.View();
        }
    }
}