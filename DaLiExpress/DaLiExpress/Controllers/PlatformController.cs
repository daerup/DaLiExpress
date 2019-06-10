﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using DaLiExpress.Models;
using DaLiExpress.UnitsOfWork;

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
    }
}