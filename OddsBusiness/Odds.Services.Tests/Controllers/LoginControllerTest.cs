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

namespace OddsBusiness.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {  
        /// <summary>
        /// Test login method py passing data model null
        /// </summary>
        [TestMethod]
        public void LoginTest_datamodelnull()
        {
            // Arrange: Setup all o\objects
            var mockRepo = new Mock<IUserRepository>();
            bool expectedresult = false;
            var data = mockRepo.Setup(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedresult);

            var loggermockRepo = new Mock<ILogger>();
            var loggerdata = loggermockRepo.Setup(x => x.Log(It.IsAny<string>(), It.IsAny<string>()));
            LoginController controller = new LoginController(mockRepo.Object,loggermockRepo.Object);

            // Act: Login the user
            HttpResponseMessage result = controller.CheckUserLogin(It.IsAny<LoginModel>());

            // Verify the method was called
            loggermockRepo.Verify(m => m.Log(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));

            mockRepo.Verify(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0)); // CheckLogin will not be verified due to model null error
           
            // Assert: 
            Assert.AreEqual(expectedresult, result);
            Assert.IsNotNull(result);
            
           
        }

        /// <summary>
        /// Test login method by passing data repository null
        /// </summary>
        [TestMethod]
        public void LoginTest_datareponull()
        {
            // Arrange: Setup all o\objects
            var mockRepo = new Mock<IUserRepository>();
            bool expectedresult = false;
            var data = mockRepo.Setup(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedresult);

            var loggermockRepo = new Mock<ILogger>();
            var loggerdata = loggermockRepo.Setup(x => x.Log(It.IsAny<string>(), It.IsAny<string>()));
            LoginController controller = new LoginController(null,loggermockRepo.Object);

            // Act: Login the user
            HttpResponseMessage result = controller.CheckUserLogin(It.IsAny<LoginModel>());

            // Verify the method was called
            loggermockRepo.Verify(m => m.Log(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));

            mockRepo.Verify(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0));

            // Assert: 
            Assert.AreEqual(expectedresult, result);
            Assert.IsNotNull(result);           

        }


        
    }
}
