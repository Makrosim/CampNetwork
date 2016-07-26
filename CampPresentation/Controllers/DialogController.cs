using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampAuth.Controllers
{
    public class DialogController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}