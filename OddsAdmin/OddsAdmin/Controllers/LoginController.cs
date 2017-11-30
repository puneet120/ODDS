using OddsAdmin.Filters;
using System.Web.Mvc;
using System.Web.Security;
using System;
using System.Web;
using OddsAdmin.Models;

namespace OddsAdmin.Web.Controllers
{
    
    public class LoginController : Controller
    {
       
        #region Index
        /// <summary>
        /// Index Method for Login
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Index Method Post for Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Result for Login</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)        {
            
            if (model.Result == true)
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                var authTicket = new FormsAuthenticationTicket(1, model.Username, DateTime.Now, DateTime.Now.AddMinutes(20), false, string.Empty);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                return RedirectToAction("Index", "Odd");

            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }
        #endregion

    
    }
}