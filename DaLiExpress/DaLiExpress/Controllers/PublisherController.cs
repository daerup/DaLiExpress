using System.Linq;
using System.Web.Mvc;
using DaLiExpress.Models;
using DaLiExpress.UnitsOfWork;
using Microsoft.Ajax.Utilities;

namespace DaLiExpress.Controllers
{
    public class PublisherController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork(new DaLi_GameExpressEntities());

        public ActionResult Index()
        {
            this.ViewBag.AllPublishers = this.unitOfWork.Publisher.GetAll();
            return this.View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            this.ViewBag.AllPublishers = this.unitOfWork.Publisher.GetAll().Where(g => g.Name.ToLower().Contains(collection["SearchedPublisher"].ToLower())).ToList();
            this.ViewBag.SearchedPublisher = collection["SearchedPublisher"];
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            return this.View();
        }

        public ActionResult Delete(int id)
        {
            this.unitOfWork.Game.GetAll().ToList().RemoveAll(g => g.PublisherID == id);
            this.unitOfWork.Publisher.Remove(this.unitOfWork.Publisher.GetById(id));
            this.unitOfWork.Complete();
            this.ViewBag.AllPublishers = this.unitOfWork.Publisher.GetAll();
            return View("Index");
        }
    }
}