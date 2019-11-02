using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OddsBusiness.Repository.Interfaces;
using OddsBusiness;
using OddsBusiness.Controllers;
using OddsBusiness.Models;
using System.Net.Http;
using System.Web.Http;

namespace OddsBusiness.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {  
        /// <summary>
        /// Test login method py passing data model null
        /// </summary>
        [TestMethod]
        public void Login_input_is_null()
        {
            // Arrange: Setup all o\objects
            var mockRepo = new Mock<IUserRepository>();
            bool expectedresult = false;
            var data = mockRepo.Setup(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedresult);

            var mockloggerRepo = new Mock<ILogger>();
            var loggerdata = mockloggerRepo.Setup(x => x.Log(It.IsAny<string>(), It.IsAny<string>()));
            LoginController controller = new LoginController(mockRepo.Object, mockloggerRepo.Object)
            {

                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act: Login the user
            HttpResponseMessage result = controller.CheckUserLogin(null);

            // Verify the method was called
            mockloggerRepo.Verify(m => m.Log(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));

            mockRepo.Verify(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0)); // CheckLogin will not be verified due to model null error

            // Assert: 
           Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.IsNotNull(result);
            
           
        }

        /// <summary>
        /// Test login method by passing data repository null
        /// </summary>
        [TestMethod]
        public void Login_repo_is_null()
        {
            // Arrange: Setup all o\objects
            var mockRepo = new Mock<IUserRepository>();
            bool expectedresult = false;
            var data = mockRepo.Setup(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedresult);

            var mockloggerRepo = new Mock<ILogger>();
            var loggerdata = mockloggerRepo.Setup(x => x.Log(It.IsAny<string>(), It.IsAny<string>()));
            LoginController controller = new LoginController(null, mockloggerRepo.Object)
            {

                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act: Login the user
            HttpResponseMessage result = controller.CheckUserLogin(It.IsAny<LoginModel>());

            // Verify the method was called
            mockloggerRepo.Verify(m => m.Log(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));

            mockRepo.Verify(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0));

            // Assert: 
            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.IsNotNull(result);           

        }


        
    }
}
