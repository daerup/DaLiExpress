using System.Web.Mvc;

namespace DaLiExpress.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
        public ActionResult Game()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
        
        
    }
}