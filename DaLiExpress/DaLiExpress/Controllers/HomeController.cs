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

        public ActionResult Edit (int id)
        {
            Game gameToEdit = this.unitOfWork.Game.GetById(id);
            this.ViewBag.GameToEdit = gameToEdit;
            this.ViewBag.Publisher = this.GetListOfPublishers(gameToEdit);
            return this.View(gameToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Game editedGame)
        {
            this.ViewBag.Message = "Gespeichert";
            this.UpdateAllPropertiesOfAGameTo(editedGame);
            this.unitOfWork.Complete();
            return this.View();
        }

        public SelectList GetListOfPublishers(Game gameToEdit)
        {
            SelectList selectList = new SelectList(this.unitOfWork.Publisher.GetAll().Select(p => p.ID).ToList());
            return selectList;
        }

        private void UpdateAllPropertiesOfAGameTo(Game updatedGame)
        {
            var oldGame = this.unitOfWork.Game.GetById(updatedGame.ID);
            oldGame.Name = updatedGame.Name;
            oldGame.Release = updatedGame.Release;
            oldGame.PublisherID = updatedGame.Publisher.ID;
        }
    }
}