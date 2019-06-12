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
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new DaLi_GameExpressEntities());

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
            if (!collection.AllKeys.Contains("Games"))
            {
                this.ViewBag.ErrorMessage = "Please select at least one Game";
            }
            else
            {
                int[] gameIDs = Array.ConvertAll(collection["Games"].Split(','), int.Parse);
                this.UpdateNonMtoMProperties(editedStudio);
                this.UpdateGames(editedStudio, gameIDs);
                this.unitOfWork.Complete();
                this.ViewBag.Message = "Saved";
            }

            this.ViewBag.Games = this.unitOfWork.Game.GetAll().ToList();
            return this.View(this.unitOfWork.DeveloperStudio.GetById(editedStudio.ID));
        }

        private void UpdateGames(DeveloperStudio updatedDeveloperStudio, int[] games)
        {
            DeveloperStudio oldDeveloperStudio = this.unitOfWork.DeveloperStudio.GetById(updatedDeveloperStudio.ID);

            oldDeveloperStudio.Game.ToList().ForEach(p => oldDeveloperStudio.Game.Remove(p));
            foreach (int gameId in games)
            {
                oldDeveloperStudio.Game.Add(this.unitOfWork.Game.GetById(gameId));
            }
        }

        private void UpdateNonMtoMProperties(DeveloperStudio updatedDeveloperStudio)
        {
            DeveloperStudio oldDeveloperStudio = this.unitOfWork.DeveloperStudio.GetById(updatedDeveloperStudio.ID);
            oldDeveloperStudio.Name = updatedDeveloperStudio.Name;
            oldDeveloperStudio.Foundingdate = updatedDeveloperStudio.Foundingdate;
        }

        public ActionResult Delete(int id)
        {
            this.unitOfWork.Game.GetAll().ForEach(g => g.DeveloperStudio.Remove(this.unitOfWork.DeveloperStudio.GetById(id)));
            this.unitOfWork.DeveloperStudio.Remove(this.unitOfWork.DeveloperStudio.GetById(id));
            this.unitOfWork.Complete();
            this.ViewBag.AllDeveloperStudios = this.unitOfWork.DeveloperStudio.GetAll();
            return View("Index");
        }

        public ActionResult Create()
        {
            this.ViewBag.Games = this.unitOfWork.Game.GetAll().ToList();
            return this.View(new DeveloperStudio{Foundingdate = DateTime.Today});
        }

        [HttpPost]
        public ActionResult Create(DeveloperStudio newStudio, FormCollection collection)
        {
            if (!collection.AllKeys.Contains("Games"))
            {
                this.ViewBag.ErrorMessage = "Please select at least one Game";
            }
            else
            {

                int[] gameIDs = Array.ConvertAll(collection["Games"].Split(','), int.Parse);
                gameIDs.ForEach(id=>newStudio.Game.Add(this.unitOfWork.Game.GetById(id)));
                this.unitOfWork.DeveloperStudio.Add(newStudio);
                this.unitOfWork.Complete();
                this.ViewBag.Message = "Developer Studio was created";
            }

            this.ViewBag.Games = this.unitOfWork.Game.GetAll().ToList();
            return this.View(this.unitOfWork.DeveloperStudio.GetById(newStudio.ID));
        }
    }
}