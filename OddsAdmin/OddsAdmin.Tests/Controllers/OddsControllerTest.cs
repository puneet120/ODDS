using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Odds.Core.Entity;
using Odds.Services.Interfaces;
using OddsAdmin;
using OddsAdmin.Controllers;
using OddsAdmin.Models;
using OddsAdmin.Web.Controllers;

namespace OddsAdmin.Tests.Controllers
{
    [TestClass]
    public class OddsControllerTest
    {  
        /// <summary>
        /// Test Odd Save Update method py passing data model null
        /// </summary>
        [TestMethod]
        public void OddSaveUpdateTest_datamodelnull()
        {
            // Arrange: Setup all o\objects
            var mockRepo = new Mock<IOddRepository>();           
            var data = mockRepo.Setup(x => x.SaveUpdateOdd(It.IsAny<Odd>())).Returns(0);

            var loggermockRepo = new Mock<ILoggerRepository>();
            var loggerdata = loggermockRepo.Setup(x => x.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()));
            OddController controller = new OddController(mockRepo.Object,loggermockRepo.Object);

            // Act: Save the Odd
            int result = controller.SaveUpdateOdd(It.IsAny<OddModel>());

            // Verify the method was called
            loggermockRepo.Verify(m => m.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));

            mockRepo.Verify(x => x.SaveUpdateOdd(It.IsAny<Odd>()), Times.Exactly(0)); // Save Update Odd method will not be verified due to model null error
           
            // Assert: 
            Assert.AreEqual(0, result);
            Assert.IsNotNull(result);
           
           
        }

        /// <summary>
        /// Test Odd Save Update method py passing data repository null
        /// </summary>
        [TestMethod]
        public void OddSaveUpdateTest_reponull()
        {
            // Arrange: Setup all o\objects
            var mockRepo = new Mock<IOddRepository>();
            var data = mockRepo.Setup(x => x.SaveUpdateOdd(It.IsAny<Odd>())).Returns(0);

            var loggermockRepo = new Mock<ILoggerRepository>();
            var loggerdata = loggermockRepo.Setup(x => x.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()));
            OddController controller = new OddController(null, loggermockRepo.Object);

            // Act: Save the Odd
            OddModel model = new OddModel();
            int result = controller.SaveUpdateOdd(model);

            // Verify the method was called
            loggermockRepo.Verify(m => m.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));

            mockRepo.Verify(x => x.SaveUpdateOdd(It.IsAny<Odd>()), Times.Exactly(0)); // Save Update Odd method will not be verified due to model null error

            // Assert: 
            Assert.AreEqual(0, result);
            Assert.IsNotNull(result);


        }

        /// <summary>
        /// Test Odd Save Update method working properly
        /// </summary>
        [TestMethod]
        public void OddSaveUpdateTest()
        {
            // Arrange: Setup all o\objects
            var mockRepo = new Mock<IOddRepository>();
            var data = mockRepo.Setup(x => x.SaveUpdateOdd(It.IsAny<Odd>())).Returns(1);

            var loggermockRepo = new Mock<ILoggerRepository>();
            var loggerdata = loggermockRepo.Setup(x => x.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()));
            OddController controller = new OddController(mockRepo.Object, loggermockRepo.Object);

            // Act: Save the Odd
            OddModel model = new OddModel();
            int result = controller.SaveUpdateOdd(model);

            // Verify the method was called
            loggermockRepo.Verify(m => m.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0));

            mockRepo.Verify(x => x.SaveUpdateOdd(It.IsAny<Odd>()), Times.Exactly(1));

            // Assert:            
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result);


        }

        /// <summary>
        /// Test GetOdds method with Request.Form null
        /// </summary>
        [TestMethod]
        public void GetOddsTest_RequestFormNull()
        {
            // Arrange: Setup all o\objects
            var mockRepo = new Mock<IOddRepository>();
            var data = mockRepo.Setup(x => x.GetOdds(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<IQueryable<Odd>>());

            var loggermockRepo = new Mock<ILoggerRepository>();
            var loggerdata = loggermockRepo.Setup(x => x.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()));
            OddController controller = new OddController(mockRepo.Object, loggermockRepo.Object);

            // Act: Save the Odd
            OddModel model = new OddModel();
            ActionResult result = controller.GetOdds();

            // Verify the method was called
            loggermockRepo.Verify(m => m.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));

            mockRepo.Verify(x => x.GetOdds(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0)); 

            // Assert:            
            Assert.IsNotNull(result);

        }


        /// <summary>
        /// Test GetOdd by id with id equals to 0
        /// </summary>
        [TestMethod]
        public void GetOddbyId_Idequals0()
        {
            // Arrange: Setup all o\objects
            var mockRepo = new Mock<IOddRepository>();
            var data = mockRepo.Setup(x => x.GetbyId(It.IsAny<int>())).Returns(new Odd());

            var loggermockRepo = new Mock<ILoggerRepository>();
            var loggerdata = loggermockRepo.Setup(x => x.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()));
            OddController controller = new OddController(mockRepo.Object, loggermockRepo.Object);

            // Act: Save the Odd
            OddModel model = new OddModel();
            ActionResult result = controller.GetOddById(0);

            // Verify the method was called
            loggermockRepo.Verify(m => m.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0));

            mockRepo.Verify(x => x.GetbyId(It.IsAny<int>()), Times.Exactly(1));

            // Assert:            
            Assert.IsNotNull(result);

        }

        /// <summary>
        /// Test DeleteOdd by id with id equals to 0
        /// </summary>
        [TestMethod]
        public void DeleteOddbyId_Idequals0()
        {
            // Arrange: Setup all o\objects
            var mockRepo = new Mock<IOddRepository>();
            var data = mockRepo.Setup(x => x.DeleteOdd(It.IsAny<int>())).Returns(1);

            var loggermockRepo = new Mock<ILoggerRepository>();
            var loggerdata = loggermockRepo.Setup(x => x.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()));
            OddController controller = new OddController(mockRepo.Object, loggermockRepo.Object);

            // Act: Save the Odd
            OddModel model = new OddModel();
            ActionResult result = controller.DeleteOdd(0);

            // Verify the method was called
            loggermockRepo.Verify(m => m.LogFileWrite(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0));

            mockRepo.Verify(x => x.DeleteOdd(It.IsAny<int>()), Times.Exactly(1));

            // Assert:            
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result);

        }
    }
}
