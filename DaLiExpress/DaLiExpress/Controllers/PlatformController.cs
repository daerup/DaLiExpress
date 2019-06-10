using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using DaLiExpress.Models;
using DaLiExpress.UnitsOfWork;
using Microsoft.Ajax.Utilities;

namespace DaLiExpress.Controllers
{
    public class PlatformController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork(new DaLi_GameExpressEntities());

        public ActionResult Index()
        {
            this.ViewBag.AllPlatforms = this.unitOfWork.Platform.GetAll();
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            return this.View();
        }

        public ActionResult Delete(int id)
        {
            this.unitOfWork.Game.GetAll().ForEach(g => g.Platform.Remove(this.unitOfWork.Platform.GetById(id)));
            this.unitOfWork.Platform.Remove(this.unitOfWork.Platform.GetById(id));
            this.unitOfWork.Complete();
            this.ViewBag.AllPlatforms = this.unitOfWork.Platform.GetAll();
            return View("Index");

        }
    }
}