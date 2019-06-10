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
            this.ViewBag.GameToEdit = gameToEdit;
            this.ViewBag.Publishers = this.GetListOfPublishers(gameToEdit);
            this.ViewBag.Platforms = unitOfWork.Platform.GetAll().ToList();
            this.ViewBag.DeveloperStudios = unitOfWork.DeveloperStudio.GetAll().ToList();
            return this.View(gameToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Game editedGame, FormCollection collection)
        {
            int[] platformIDs = Array.ConvertAll(collection["Platforms"].Split(','), int.Parse);
            int[] developerStudios = Array.ConvertAll(collection["DeveloperStudios"].Split(','), int.Parse);

            this.ViewBag.Message = "Gespeichert";
            this.ViewBag.Publishers = this.GetListOfPublishers(editedGame);
            this.ViewBag.Platforms = unitOfWork.Platform.GetAll();
            this.ViewBag.DeveloperStudios = unitOfWork.DeveloperStudio.GetAll().ToList();
            this.UpdateNonMtoMProperties(editedGame);
            this.UpdatePlatforms(editedGame, platformIDs);
            this.UpdateDeveloperStudios(editedGame, developerStudios);
            this.unitOfWork.Complete();

            return this.View(unitOfWork.Game.GetById(editedGame.ID));
        }

        public List<SelectListItem> GetListOfPublishers(Game gameToEdit)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (Publisher publisher in unitOfWork.Publisher.GetAll())
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
            foreach (DeveloperStudio developerStudio in unitOfWork.DeveloperStudio.GetAll())
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
                oldGame.Platform.Add(unitOfWork.Platform.GetById(platformId));
            }
        }
        private void UpdateDeveloperStudios(Game updatedGame, int[] developerStudios)
        {
            Game oldGame = this.unitOfWork.Game.GetById(updatedGame.ID);
            
            oldGame.DeveloperStudio.ToList().ForEach(p=>oldGame.DeveloperStudio.Remove(p));
            foreach (int developerStudioId in developerStudios)
            {
                oldGame.DeveloperStudio.Add(unitOfWork.DeveloperStudio.GetById(developerStudioId));
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