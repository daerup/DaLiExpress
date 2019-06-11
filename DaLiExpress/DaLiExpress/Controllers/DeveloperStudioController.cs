using System;
using System.Linq;
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

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            this.ViewBag.AllDeveloperStudios = this.unitOfWork.DeveloperStudio.GetAll().Where(g => g.Name.ToLower().Contains(collection["SearchedDeveloper"].ToLower())).ToList();
            this.ViewBag.SearchedDeveloper = collection["SearchedDeveloper"];
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            DeveloperStudio studioToEdit = this.unitOfWork.DeveloperStudio.GetById(id);
            this.ViewBag.Games = this.unitOfWork.Game.GetAll().ToList();
            return this.View(studioToEdit);
        }

        [HttpPost]
        public ActionResult Edit(DeveloperStudio editedStudio, FormCollection collection)
        {
            int[] gameIDs = Array.ConvertAll(collection["DeveloperStudios"].Split(','), int.Parse);
            this.UpdateNonMtoMProperties(editedStudio);
            this.UpdatePlatforms(editedStudio, gameIDs);
            this.ViewBag.Games = this.unitOfWork.Game.GetAll().ToList();
            this.unitOfWork.Complete();
            this.ViewBag.Message = "Gespeichert";

            return this.View(this.unitOfWork.DeveloperStudio.GetById(editedStudio.ID));
        }

        private void UpdatePlatforms(DeveloperStudio updateDeveloperStudio, int[] games)
        {
            DeveloperStudio oldDeveloperStudio = this.unitOfWork.DeveloperStudio.GetById(updateDeveloperStudio.ID);

            oldDeveloperStudio.Game.ToList().ForEach(p => oldDeveloperStudio.Game.Remove(p));
            foreach (int gameId in games)
            {
                oldDeveloperStudio.Game.Add(this.unitOfWork.Game.GetById(gameId));
            }
        }

        private void UpdateNonMtoMProperties(DeveloperStudio updateDeveloperStudio)
        {
            DeveloperStudio oldDeveloperStudio = this.unitOfWork.DeveloperStudio.GetById(updateDeveloperStudio.ID);
            oldDeveloperStudio.Name = updateDeveloperStudio.Name;
            oldDeveloperStudio.Foundingdate = updateDeveloperStudio.Foundingdate;
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