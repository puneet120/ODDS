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
        private IUserRepository _repo;
        private ILogger _loggerrepo;

        public LoginController(IUserRepository repo, ILogger loggerrepo
          )
        {
            this._repo = repo;
            this._loggerrepo = loggerrepo;
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
                _loggerrepo.Log(ex.Message, ex.StackTrace);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }

        }
    }
}
