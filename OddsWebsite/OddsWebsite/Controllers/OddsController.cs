using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OddsWebsite.Controllers
{
    public class OddsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }        
    }
}