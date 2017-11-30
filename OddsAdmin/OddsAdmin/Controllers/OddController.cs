using OddsAdmin.Filters;
using System.Linq;
using System.Web.Mvc;
using OddsAdmin.Models;
using System.Web.Security;
using System;
using System.Web;


namespace OddsAdmin.Controllers
{
    [Authorize]  
    public class OddController : Controller
    {

        #region Index

        /// <summary>
        /// Odd Display
        /// </summary>
        /// <returns>Odd View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region LogOut

        /// <summary>
        /// Log out User & Clear Session Cookie
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        #endregion
    }

}