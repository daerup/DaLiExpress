using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using DaLiExpress.Models;
using DaLiExpress.Repositories;
using DaLiExpress.UnitsOfWork;
using Microsoft.Ajax.Utilities;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace DaLiExpress.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork(new DaLi_GameExpressEntities());

        public ActionResult Index()
        {
            this.ViewBag.AllGames = this.unitOfWork.Game.GetAll();
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            Game gameToEdit = this.unitOfWork.Game.GetById(id);
            this.PrepareViewBag(gameToEdit);
            return this.View(gameToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Game editedGame, FormCollection collection)
        {
            if (!collection.AllKeys.Contains("Platforms"))
            {
                this.ViewBag.ErrorMessage = "Please select at least one Platform";
                this.PrepareViewBag(editedGame);
                return this.View(this.unitOfWork.Game.GetById(editedGame.ID));
            }
            if (!collection.AllKeys.Contains("DeveloperStudios"))
            {
                this.ViewBag.ErrorMessage = "Please select at least one Developer studio";
                this.PrepareViewBag(editedGame);
                return this.View(this.unitOfWork.Game.GetById(editedGame.ID));
            }

            int[] platformIDs = Array.ConvertAll(collection["Platforms"].Split(','), int.Parse);
            int[] developerStudioIDs = Array.ConvertAll(collection["DeveloperStudios"].Split(','), int.Parse);

            this.UpdateNonMtoMProperties(editedGame);
            this.UpdatePlatforms(editedGame, platformIDs);
            this.UpdateDeveloperStudios(editedGame, developerStudioIDs);
            this.PrepareViewBag(editedGame);
            this.unitOfWork.Complete();
            this.ViewBag.Message = "Gespeichert";

            return this.View(this.unitOfWork.Game.GetById(editedGame.ID));
        }

        private void PrepareViewBag(Game game)
        {
            this.ViewBag.Publishers = this.GetListOfPublishers(game);
            this.ViewBag.Platforms = this.unitOfWork.Platform.GetAll();
            this.ViewBag.DeveloperStudios = this.unitOfWork.DeveloperStudio.GetAll().ToList();
        }

        public List<SelectListItem> GetListOfPublishers(Game gameToEdit)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (Publisher publisher in this.unitOfWork.Publisher.GetAll())
            {
                selectList.Add(new SelectListItem()
                {
                    Value = publisher.ID.ToString(),
                    Text = publisher.Name
                });
            }

            return selectList;
        }

        public List<SelectListItem> GetListOfDeveloperStudios(Game gameToEdit)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (DeveloperStudio developerStudio in this.unitOfWork.DeveloperStudio.GetAll())
            {
                selectList.Add(new SelectListItem()
                {
                    Value = developerStudio.ID.ToString(),
                    Text = developerStudio.Name
                });
            }
            return selectList;
        }

        private void UpdateNonMtoMProperties(Game updatedGame)
        {
            Game oldGame = this.unitOfWork.Game.GetById(updatedGame.ID);
            oldGame.Name = updatedGame.Name;
            oldGame.Rating = updatedGame.Rating;
            oldGame.Release = updatedGame.Release;
            oldGame.PublisherID = updatedGame.Publisher.ID;
        }

        private void UpdatePlatforms(Game updatedGame, int[] platforms)
        {
            Game oldGame = this.unitOfWork.Game.GetById(updatedGame.ID);
            
            oldGame.Platform.ToList().ForEach(p=>oldGame.Platform.Remove(p));
            foreach (int platformId in platforms)
            {
                oldGame.Platform.Add(this.unitOfWork.Platform.GetById(platformId));
            }
        }
        private void UpdateDeveloperStudios(Game updatedGame, int[] developerStudios)
        {
            Game oldGame = this.unitOfWork.Game.GetById(updatedGame.ID);
            
            oldGame.DeveloperStudio.ToList().ForEach(p=>oldGame.DeveloperStudio.Remove(p));
            foreach (int developerStudioId in developerStudios)
            {
                oldGame.DeveloperStudio.Add(this.unitOfWork.DeveloperStudio.GetById(developerStudioId));
            }
        }

        public ActionResult Delete(int id)
        {
            this.unitOfWork.Publisher.GetAll().ForEach(p => p.Game.Remove(this.unitOfWork.Game.GetById(id)));
            this.unitOfWork.Platform.GetAll().ForEach(p => p.Game.Remove(this.unitOfWork.Game.GetById(id)));
            this.unitOfWork.DeveloperStudio.GetAll().ForEach(d => d.Game.Remove(this.unitOfWork.Game.GetById(id)));
            this.unitOfWork.Game.Remove(this.unitOfWork.Game.GetById(id));
            this.unitOfWork.Complete();
            this.ViewBag.AllGames = this.unitOfWork.Game.GetAll();
            return View("Index");
        }
    }
}