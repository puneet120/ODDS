﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using OddsBusiness.Core.Entity;
using OddsBusiness.Models;
using OddsBusiness.Repository.Interfaces;

namespace OddsBusiness.Controllers
{
    public class OddController : ApiController
    {
        private IOddRepository _repo;
        private ILoggerRepository _loggerrepo;
        public OddController(IOddRepository repo, ILoggerRepository loggerrepo)
        {
            this._repo = repo;
            this._loggerrepo = loggerrepo;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        #region Delete Odd
        /// <summary>
        /// Delete Odd from Grid
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON with success/failure</returns>
        [HttpPost]
        [Route("odd/delete")]
        public HttpResponseMessage DeleteOdd(OddsModel model)
        {
            try
            {
                var result = _repo.DeleteOdd(model.Id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _loggerrepo.LogFileWrite(ex.Message, ex.StackTrace);
                return Request.CreateResponse(HttpStatusCode.OK, 0);
            }
        }

        #endregion

        #region Get Odd by ID

        /// <summary>
        /// Get Odd by Odd ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON object for Odd</returns>
        [HttpPost]
        [Route("odd/getbyid")]
        public HttpResponseMessage GetOddById(OddsModel model)
        {
            try
            {
                Odd odd = _repo.GetbyId(model.Id);
                return Request.CreateResponse(HttpStatusCode.OK, odd);
            }
            catch (Exception ex)
            {
                _loggerrepo.LogFileWrite(ex.Message, ex.StackTrace);
                return Request.CreateResponse(HttpStatusCode.OK, new Odd());
            }

        }

        //getoddbyid_ when nullpassed -- returns null
        //getoddbyid_when negative id passed - returns null
        //getoddby id_when repo is not responding -- should not throw exception

        #endregion

        #region GetOdds

        /// <summary>
        /// Get all Odds and load in Data Table
        /// </summary>
        /// <returns>JSON Result</returns>
        [HttpPost]
        [Route("odds/get")]
        public HttpResponseMessage GetOdds()
        {
            try
            {
                var httpContext = (HttpContextWrapper)Request.Properties["MS_HttpContext"];
                string searchvalue = httpContext.Request.Form.GetValues("search[value]").FirstOrDefault();
                var draw = httpContext.Request.Form.GetValues("draw").FirstOrDefault();
                var start = httpContext.Request.Form.GetValues("start").FirstOrDefault();
                var length = httpContext.Request.Form.GetValues("length").FirstOrDefault();
                //Get Sort columns values when we click on Header Name of column
                //getting column name

                string sortColumn = string.Empty;
                string sortColumnDir = string.Empty;

                var sortColumnDirection = httpContext.Request.Form.GetValues("order[0][dir]");
                if (sortColumnDirection != null)
                {
                    sortColumnDir = sortColumnDirection.FirstOrDefault();
                    sortColumn = httpContext.Request.Form.GetValues("columns[" + httpContext.Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                }

                //Soring direction(either desending or ascending)


                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int totalRecords = 0;

                string descSearch = string.Empty, odd_1search = string.Empty, odd_xsearch = string.Empty, odd_2search = string.Empty;



                if (httpContext.Request.Form.AllKeys.Contains("Description"))
                {
                    descSearch = httpContext.Request.Form.GetValues("Description").FirstOrDefault();
                }
                else
                {
                    descSearch = "NA";
                }
                if (httpContext.Request.Form.AllKeys.Contains("Odd_1"))
                {

                    odd_1search = httpContext.Request.Form.GetValues("Odd_1").FirstOrDefault();
                }
                else
                {
                    odd_1search = "NA";
                }
                if (httpContext.Request.Form.AllKeys.Contains("Odd_X"))
                {

                    odd_xsearch = httpContext.Request.Form.GetValues("Odd_X").FirstOrDefault();
                }
                else
                {
                    odd_xsearch = "NA";
                }
                if (httpContext.Request.Form.AllKeys.Contains("Odd_2"))
                {

                    odd_2search = httpContext.Request.Form.GetValues("Odd_2").FirstOrDefault();
                }
                else
                {
                    odd_2search = "NA";
                }

                var v = _repo.GetOdds(searchvalue, descSearch, odd_1search, odd_xsearch, odd_2search);

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
                return Request.CreateResponse(HttpStatusCode.OK, new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data });
            }
            catch (Exception ex)
            {
                _loggerrepo.LogFileWrite(ex.Message, ex.StackTrace);
                return Request.CreateResponse(HttpStatusCode.OK, new List<object>());
            }


        }

        #endregion

        [HttpPost]
        [Route("odds/save")]
        public HttpResponseMessage SaveUpdateOdd(OddsModel model)
        {

            try
            {
                Odd odd = new Odd();
                odd.Description = model.Description;
                odd.Odd_1 = Convert.ToDecimal(model.Odd_1);
                odd.Odd_X = Convert.ToDecimal(model.Odd_X);
                odd.Odd_2 = Convert.ToDecimal(model.Odd_2);
                odd.Id = model.Id;
                var result = _repo.SaveUpdateOdd(odd);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _loggerrepo.LogFileWrite(ex.Message, ex.StackTrace);
                return Request.CreateResponse(HttpStatusCode.OK, 0);
            }
        }

        [HttpPost]
        [Route("odds/public")]
        public HttpResponseMessage GetOddsforPublic()
        {
            var data = _repo.GetOdds(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}
