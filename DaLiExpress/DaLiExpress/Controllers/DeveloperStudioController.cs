using System.Web.Mvc;
using DaLiExpress.Models;
using DaLiExpress.UnitsOfWork;
using Microsoft.Ajax.Utilities;

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

        public ActionResult Delete(int id)
        {
            this.unitOfWork.Game.GetAll().ForEach(g => g.DeveloperStudio.Remove(this.unitOfWork.DeveloperStudio.GetById(id)));
            this.unitOfWork.DeveloperStudio.Remove(this.unitOfWork.DeveloperStudio.GetById(id));
            this.unitOfWork.Complete();
            this.ViewBag.AllDeveloperStudios = this.unitOfWork.DeveloperStudio.GetAll();
            return View("Index");
        }
    }
}