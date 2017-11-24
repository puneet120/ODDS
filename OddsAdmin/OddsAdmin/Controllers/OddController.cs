using Odds.Services.Interfaces;
using Odds.Core.Entity;
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
    [ExceptionFilter]
    public class OddController : Controller
    {
        private IOddRepository _repo;
        public OddController(IOddRepository repo)
        {
            this._repo = repo;
        }
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

        /// <summary>
        /// Save/Update Odds
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Odd View with Success/Failure</returns>
        [HttpPost]       
        public ActionResult Index(OddModel model)
        {
            var result = SaveUpdateOdd(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        [NonAction]
        public int SaveUpdateOdd(OddModel model)
        {
            Odd odd = new Odd();
            odd.Description = model.Description;
            odd.Odd_1 = model.Odd_1;
            odd.Odd_X = model.Odd_X;
            odd.Odd_2 = model.Odd_2;
            odd.Id = model.Id;
            var result = _repo.SaveUpdateOdd(odd);
            return result;
        }

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

        #region GetOdds

        /// <summary>
        /// Get all Odds and load in Data Table
        /// </summary>
        /// <returns>JSON Result</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GetOdds()
        {

            string searchvalue = Request.Form.GetValues("search[value]").FirstOrDefault();
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Get Sort columns values when we click on Header Name of column
            //getting column name

            string sortColumn = string.Empty;
            string sortColumnDir = string.Empty;

            var sortColumnDirection = Request.Form.GetValues("order[0][dir]");
            if (sortColumnDirection != null)
            {
                sortColumnDir = sortColumnDirection.FirstOrDefault();
                sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            }
          
            //Soring direction(either desending or ascending)
           

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;

            string descSearch = string.Empty, odd_1search = string.Empty,odd_xsearch=string.Empty,odd_2search=string.Empty;

           

            if (Request.Form.AllKeys.Contains("Description"))
            {
                descSearch = Request.Form.GetValues("Description").FirstOrDefault();
            }
            else
            {
                descSearch = "NA";
            }
            if (Request.Form.AllKeys.Contains("Odd_1"))
            {

                odd_1search = Request.Form.GetValues("Odd_1").FirstOrDefault();
            }
            else
            {
                odd_1search = "NA";
            }
            if (Request.Form.AllKeys.Contains("Odd_X"))
            {

                odd_xsearch = Request.Form.GetValues("Odd_X").FirstOrDefault();
            }
            else
            {
                odd_xsearch = "NA";
            }
            if (Request.Form.AllKeys.Contains("Odd_2"))
            {

                odd_2search = Request.Form.GetValues("Odd_2").FirstOrDefault();
            }
            else
            {
                odd_2search = "NA";
            }

            var v = _repo.GetOdds(searchvalue,descSearch,odd_1search,odd_xsearch,odd_2search);

            if (!string.IsNullOrEmpty(sortColumn) && sortColumn == "Description")
            {
                if (sortColumnDir == "desc")
                    v = v.OrderByDescending(j => j.Description);
                else
                    v = v.OrderBy(j => j.Description);
            }
            if (!string.IsNullOrEmpty(sortColumn) && sortColumn == "Odd_1")
            {
                if (sortColumnDir == "desc")
                    v = v.OrderByDescending(j => j.Odd_1);
                else
                    v = v.OrderBy(j => j.Odd_1);
            }
            if (!string.IsNullOrEmpty(sortColumn) && sortColumn == "Odd_X")
            {
                if (sortColumnDir == "desc")
                    v = v.OrderByDescending(j => j.Odd_X);
                else
                    v = v.OrderBy(j => j.Odd_X);
            }
            if (!string.IsNullOrEmpty(sortColumn) && sortColumn == "Odd_2")
            {
                if (sortColumnDir == "desc")
                    v = v.OrderByDescending(j => j.Odd_2);
                else
                    v = v.OrderBy(j => j.Odd_2);
            }

            totalRecords = v.Count();
            var data = v.Skip(skip).Take(pageSize).ToList();


            //retring the data for server side pagination
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region Get Odd by ID

        /// <summary>
        /// Get Odd by Odd ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON object for Odd</returns>
        public ActionResult GetOddById(int id)
        {
            Odd odd = _repo.GetbyId(id);
            return Json(odd, JsonRequestBehavior.AllowGet);
        }

        //getstockbyid_ when nullpassed -- returns null
        //getstockbyid_when negative id passed - returns null
        //getstockby id_when repo is not responding -- should not throw exception

        #endregion

        #region Delete Odd
        /// <summary>
        /// Delete Odd from Grid
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON with success/failure</returns>
        public ActionResult DeleteOdd(int id)
        {
            var result = _repo.DeleteOdd(id);            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Get all Odds data

        /// <summary>
        /// Get Odd data to display on front end
        /// </summary>
        /// <returns>JSON of Odd data</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAllData()
        {
            var result = _repo.GetOdds(string.Empty, string.Empty, string.Empty,string.Empty,string.Empty).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
    }
}