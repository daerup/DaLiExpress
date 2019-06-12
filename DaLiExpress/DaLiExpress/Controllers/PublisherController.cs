using System;
using System.Linq;
using System.Web.Mvc;
using DaLiExpress.Models;
using DaLiExpress.UnitsOfWork;
using Microsoft.Ajax.Utilities;

namespace DaLiExpress.Controllers
{
    public class PublisherController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new DaLi_GameExpressEntities());

        public ActionResult Index()
        {
            this.ViewBag.AllPublishers = this.unitOfWork.Publisher.GetAll();
            return this.View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            this.ViewBag.AllPublishers = this.unitOfWork.Publisher.GetAll().Where(g => g.Name.ToLower().Contains(collection["SearchedPublisher"].ToLower())).ToList();
            this.ViewBag.SearchedPublisher = collection["SearchedPublisher"];
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            Publisher platformToEdit = this.unitOfWork.Publisher.GetById(id);
            this.ViewBag.Games = this.unitOfWork.Game.GetAll().ToList();
            return this.View(platformToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Publisher editedPublisher, FormCollection collection)
        {
            if (!collection.AllKeys.Contains("Games"))
            {
                this.ViewBag.ErrorMessage = "Please select at least one Game";
            }
            else
            {
                int[] gameIDs = Array.ConvertAll(collection["Games"].Split(','), int.Parse);
                this.UpdateNonMtoMProperties(editedPublisher);
                this.UpdateGames(editedPublisher, gameIDs);
                this.unitOfWork.Complete();
                this.ViewBag.Message = "Saved";
            }

            this.ViewBag.Games = this.unitOfWork.Game.GetAll().ToList();
            return this.View(this.unitOfWork.Publisher.GetById(editedPublisher.ID));
        }

        private void UpdateGames(Publisher updatedPlatform, int[] games)
        {
            Publisher oldPlatform = this.unitOfWork.Publisher.GetById(updatedPlatform.ID);

            oldPlatform.Game.ToList().ForEach(p => oldPlatform.Game.Remove(p));
            foreach (int gameId in games)
            {
                oldPlatform.Game.Add(this.unitOfWork.Game.GetById(gameId));
            }
        }

        private void UpdateNonMtoMProperties(Publisher updatedPlatform)
        {
            Publisher oldPublisher = this.unitOfWork.Publisher.GetById(updatedPlatform.ID);
            oldPublisher.Name = updatedPlatform.Name;
            oldPublisher.Foundingdate = updatedPlatform.Foundingdate;
        }

        public ActionResult Delete(int id)
        {
            this.unitOfWork.Game.GetAll().ToList().RemoveAll(g => g.PublisherID == id);
            this.unitOfWork.Publisher.Remove(this.unitOfWork.Publisher.GetById(id));
            this.unitOfWork.Complete();
            this.ViewBag.AllPublishers = this.unitOfWork.Publisher.GetAll();
            return this.View("Index");
        }
    }
}