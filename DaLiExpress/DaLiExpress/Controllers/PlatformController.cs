using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq;
using System.Web.Mvc;
using DaLiExpress.Models;
using DaLiExpress.UnitsOfWork;
using Microsoft.Ajax.Utilities;

namespace DaLiExpress.Controllers
{
    public class PlatformController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new DaLi_GameExpressEntities());

        public ActionResult Index()
        {
            this.ViewBag.AllPlatforms = this.unitOfWork.Platform.GetAll();
            return this.View();
        }


        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            this.ViewBag.AllPlatforms = this.unitOfWork.Platform.GetAll().Where(g => g.Name.ToLower().Contains(collection["SearchedPlatforms"].ToLower())).ToList();
            this.ViewBag.SearchedPlatforms = collection["SearchedPlatforms"];
            return this.View();
        }


        public ActionResult Edit(int id)
        {
            Platform platformToEdit = this.unitOfWork.Platform.GetById(id);
            this.ViewBag.Games = this.unitOfWork.Game.GetAll().ToList();
            return this.View(platformToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Platform editedPlatform, FormCollection collection)
        {
            if (!collection.AllKeys.Contains("Games"))
            {
                this.ViewBag.ErrorMessage = "Please select at least one Game";
            }
            else
            {
                int[] gameIDs = Array.ConvertAll(collection["Games"].Split(','), int.Parse);
                this.UpdateNonMtoMProperties(editedPlatform);
                this.UpdateGames(editedPlatform, gameIDs);
                this.unitOfWork.Complete();
                this.ViewBag.Message = "Saved";
            }

            this.ViewBag.Games = this.unitOfWork.Game.GetAll().ToList();
            return this.View(this.unitOfWork.Platform.GetById(editedPlatform.ID));
        }

        private void UpdateGames(Platform updatedPlatform, int[] games)
        {
            Platform oldPlatform = this.unitOfWork.Platform.GetById(updatedPlatform.ID);

            oldPlatform.Game.ToList().ForEach(p => oldPlatform.Game.Remove(p));
            foreach (int gameId in games)
            {
                oldPlatform.Game.Add(this.unitOfWork.Game.GetById(gameId));
            }
        }

        private void UpdateNonMtoMProperties(Platform updatedPlatform)
        {
            Platform oldPlatform = this.unitOfWork.Platform.GetById(updatedPlatform.ID);
            oldPlatform.Name = updatedPlatform.Name;
        }

        public ActionResult Delete(int id)
        {
            this.unitOfWork.Game.GetAll().ForEach(g => g.Platform.Remove(this.unitOfWork.Platform.GetById(id)));
            this.unitOfWork.Platform.Remove(this.unitOfWork.Platform.GetById(id));
            this.unitOfWork.Complete();
            this.ViewBag.AllPlatforms = this.unitOfWork.Platform.GetAll();
            return View("Index");
        }

        public ActionResult Create()
        {
            this.ViewBag.Games = this.unitOfWork.Game.GetAll().ToList();
            return this.View(new Platform());
        }

        [HttpPost]
        public ActionResult Create(Platform newPlatform, FormCollection collection)
        {
            if (!collection.AllKeys.Contains("Games"))
            {
                this.ViewBag.ErrorMessage = "Please select at least one Game";
            }
            else
            {

                int[] gameIDs = Array.ConvertAll(collection["Games"].Split(','), int.Parse);
                gameIDs.ForEach(id => newPlatform.Game.Add(this.unitOfWork.Game.GetById(id)));
                this.unitOfWork.Platform.Add(newPlatform);
                this.unitOfWork.Complete();
                this.ViewBag.Message = "Platform was created";
            }

            this.ViewBag.Games = this.unitOfWork.Game.GetAll().ToList();
            return this.View(this.unitOfWork.Platform.GetById(newPlatform.ID));
        }
    }
}