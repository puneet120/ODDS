using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Odds.Services.Interfaces;
using OddsAdmin;
using OddsAdmin.Controllers;
using OddsAdmin.Models;
using OddsAdmin.Web.Controllers;

namespace OddsAdmin.Tests.Controllers
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
            var mockRepo = new Mock<ILoginRepository>();
            bool expectedresult = false;
            var data = mockRepo.Setup(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedresult);

            var loggermockRepo = new Mock<ILoggerRepository>();
            var loggerdata = loggermockRepo.Setup(x => x.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()));
            LoginController controller = new LoginController(mockRepo.Object,loggermockRepo.Object);

            // Act: Login the user
            bool result = controller.CheckUserLogin(It.IsAny<LoginModel>());

            // Verify the method was called
            loggermockRepo.Verify(m => m.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));

            mockRepo.Verify(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0)); // CheckLogin will not be verified due to model null error
           
            // Assert: 
            Assert.AreEqual(expectedresult, result);
            Assert.IsNotNull(result);
            Assert.IsFalse(result);
           
        }

        /// <summary>
        /// Test login method by passing data repository null
        /// </summary>
        [TestMethod]
        public void LoginTest_datareponull()
        {
            // Arrange: Setup all o\objects
            var mockRepo = new Mock<ILoginRepository>();
            bool expectedresult = false;
            var data = mockRepo.Setup(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedresult);

            var loggermockRepo = new Mock<ILoggerRepository>();
            var loggerdata = loggermockRepo.Setup(x => x.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()));
            LoginController controller = new LoginController(null,loggermockRepo.Object);

            // Act: Login the user
            bool result = controller.CheckUserLogin(It.IsAny<LoginModel>());

            // Verify the method was called
            loggermockRepo.Verify(m => m.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));

            mockRepo.Verify(x => x.CheckLogin(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0));

            // Assert: 
            Assert.AreEqual(expectedresult, result);
            Assert.IsNotNull(result);
            Assert.IsFalse(result);

        }


        
    }
}
