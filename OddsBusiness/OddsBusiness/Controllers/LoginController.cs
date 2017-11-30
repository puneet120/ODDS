using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OddsBusiness.Models;
using OddsBusiness.Repository.Interfaces;

namespace OddsBusiness.Controllers
{
    public class LoginController : ApiController
    {
        private ILoginRepository _repo;
        private ILoggerRepository _loggerrepo;

        public LoginController(ILoginRepository repo, ILoggerRepository loggerrepo
          )
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

        [HttpPost]
        [Route("login/user")]
        public HttpResponseMessage CheckUserLogin(LoginModel model)
        {
            try
            {
                var result = _repo.CheckLogin(model.Username, model.Password);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _loggerrepo.LogFileWrite(ex.Message, ex.StackTrace);
                return Request.CreateResponse(HttpStatusCode.OK, false);
            }

        }
    }
}
