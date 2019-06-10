using System.Web.Mvc;
using DaLiExpress.Models;
using DaLiExpress.UnitsOfWork;

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

        public ActionResult Edit(int id)
        {
            return this.View();
        }
    }
}