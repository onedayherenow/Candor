﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Candor.Controllers
{
    public class IdeaController : Controller
    {
        // GET: Idea
        public ActionResult Index()
        {
            return View();
        }
    }
}