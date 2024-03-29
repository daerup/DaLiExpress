using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using DaLiExpress.Models;
using DaLiExpress.UnitsOfWork;
using Microsoft.Ajax.Utilities;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace DaLiExpress.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new DaLi_GameExpressEntities());

        public ActionResult Index()
        {
            this.ViewBag.AllGames = this.unitOfWork.Game.GetAll();
            return this.View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            this.ViewBag.AllGames = this.unitOfWork.Game.GetAll().Where(g=>g.Name.ToLower().Contains(collection["SearchedGame"].ToLower())).ToList();
            this.ViewBag.SearchedGame = collection["SearchedGame"];
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            Game gameToEdit = this.unitOfWork.Game.GetById(id);
            this.PrepareViewBag();
            return this.View(gameToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Game editedGame, FormCollection collection)
        {
            if (!collection.AllKeys.Contains("Platforms"))
            {
                this.ModelState.AddModelError("Platform", "Please select at least one Platform");
            }
            else if (!collection.AllKeys.Contains("DeveloperStudios"))
            {
                this.ModelState.AddModelError("DeveloperStudio", "Please select at least one Developer studio");
            }
            else
            {
                int[] platformIDs = Array.ConvertAll(collection["Platforms"].Split(','), int.Parse);
                int[] developerStudioIDs = Array.ConvertAll(collection["DeveloperStudios"].Split(','), int.Parse);

                this.UpdateNonMtoMProperties(editedGame);
                this.UpdatePlatforms(editedGame, platformIDs);
                this.UpdateDeveloperStudios(editedGame, developerStudioIDs);
                this.unitOfWork.Complete();
                this.ViewBag.Message = "Saved";
            }

            this.PrepareViewBag();
            return this.View(this.unitOfWork.Game.GetById(editedGame.ID));
        }

        public ActionResult Delete(int id)
        {
            this.unitOfWork.Publisher.GetAll().ForEach(p => p.Game.Remove(this.unitOfWork.Game.GetById(id)));
            this.unitOfWork.Platform.GetAll().ForEach(p => p.Game.Remove(this.unitOfWork.Game.GetById(id)));
            this.unitOfWork.DeveloperStudio.GetAll().ForEach(d => d.Game.Remove(this.unitOfWork.Game.GetById(id)));
            this.unitOfWork.Game.Remove(this.unitOfWork.Game.GetById(id));
            this.unitOfWork.Complete();
            this.ViewBag.AllGames = this.unitOfWork.Game.GetAll();
            return this.View("Index");
        }

        public ActionResult Create()
        {
            this.PrepareViewBag();
            return this.View(new Game { Release = DateTime.Today });
        }

        [HttpPost]
        public ActionResult Create(Game newGame, FormCollection collection)
        {
            if (!collection.AllKeys.Contains("Platforms"))
            {
                this.ModelState.AddModelError("Platform", "Please select at least one Platform");
            }
            else if (!collection.AllKeys.Contains("DeveloperStudios"))
            {
                this.ModelState.AddModelError("DeveloperStudio", "Please select at least one Developer studio");
            }
            else
            {
                int[] platformIDs = Array.ConvertAll(collection["Platforms"].Split(','), int.Parse);
                int[] developerStudioIDs = Array.ConvertAll(collection["DeveloperStudios"].Split(','), int.Parse);

                newGame.PublisherID = newGame.Publisher.ID;
                newGame.Publisher = null;
                platformIDs.ForEach(id => newGame.Platform.Add(this.unitOfWork.Platform.GetById(id)));
                developerStudioIDs.ForEach(id => newGame.DeveloperStudio.Add(this.unitOfWork.DeveloperStudio.GetById(id)));
                this.unitOfWork.Game.Add(newGame);
                this.unitOfWork.Complete();
                this.ViewBag.Message = "Game was created";

            }

            this.PrepareViewBag();
            return this.View(newGame);
        }

        private void PrepareViewBag()
        {
            this.ViewBag.Publishers = this.GetListOfPublishers();
            this.ViewBag.Platforms = this.unitOfWork.Platform.GetAll();
            this.ViewBag.DeveloperStudios = this.unitOfWork.DeveloperStudio.GetAll().ToList();
        }

        public List<SelectListItem> GetListOfPublishers()
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
    }
}