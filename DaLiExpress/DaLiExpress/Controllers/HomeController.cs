using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Game()
        {
            this.ViewBag.Message = "Your contact page.";
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            Game gameToEdit = this.unitOfWork.Game.GetById(id);
            this.ViewBag.GameToEdit = gameToEdit;
            this.ViewBag.Publishers = this.GetListOfPublishers(gameToEdit);
            this.ViewBag.Platforms = this.GetListOfPlatforms(gameToEdit);
            this.ViewBag.DeveloperStudios = this.GetListOfDeveloperStudios(gameToEdit);
            return this.View(gameToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Game editedGame)
        {
            this.ViewBag.Message = "Gespeichert";
            this.ViewBag.Publishers = this.GetListOfPublishers(editedGame);
            this.ViewBag.Platforms = this.GetListOfPlatforms(editedGame);
            this.ViewBag.DeveloperStudios = this.GetListOfDeveloperStudios(editedGame);
            this.UpdateAllPropertiesOfAGameTo(editedGame);
            this.unitOfWork.Complete();
            return this.View();
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

        public List<SelectListItem> GetListOfPlatforms(Game gameToEdit)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (Platform platform in unitOfWork.Platform.GetAll())
            {
                selectList.Add(new SelectListItem()
                {
                    Value = platform.ID.ToString(),
                    Text = platform.Name
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

        private void UpdateAllPropertiesOfAGameTo(Game updatedGame)
        {
            var oldGame = this.unitOfWork.Game.GetById(updatedGame.ID);
            oldGame.Name = updatedGame.Name;
            oldGame.Rating = updatedGame.Rating;
            oldGame.Release = updatedGame.Release;
            oldGame.PublisherID = updatedGame.Publisher.ID;
        }
    }
}