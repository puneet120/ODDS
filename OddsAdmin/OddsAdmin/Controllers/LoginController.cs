using Odds.Services.Interfaces;
using OddsAdmin.Filters;
using System.Web.Mvc;
using System.Web.Security;
using System;
using System.Web;
using OddsAdmin.Models;

namespace OddsAdmin.Web.Controllers
{
    [ExceptionFilter]
    public class LoginController : Controller
    {
        private ILoginRepository _repo;
        private ILoggerRepository _loggerrepo;

        public LoginController(ILoginRepository repo,
            ILoggerRepository loggerrepo)
        {
            this._repo = repo;
            _loggerrepo = loggerrepo;
        }

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
        public ActionResult Index(LoginModel model)
        {
            var result = CheckUserLogin(model);
            if (result == true)
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                var authTicket = new FormsAuthenticationTicket(1, model.Username, DateTime.Now, DateTime.Now.AddMinutes(20), false, string.Empty);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                return RedirectToAction("Index", "Stock");

            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }
        #endregion

        [NonAction]
        public bool CheckUserLogin(LoginModel model)
        {
            try
            {
                var result = _repo.CheckLogin(model.Username, model.Password);
                //if (result == true)
                //{
                   
                //    _loggerrepo.LogFileWrite("Successfull", string.Empty);
                //    //  System.IO.File.ReadAllText
                //}
                //else
                //{
                //    _loggerrepo.LogFileWrite("Invalid Attempt", string.Empty);


                //}
                return result;
            }
            catch (Exception ex)
            {
                return true;
            }

        }
    }
}