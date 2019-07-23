using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PaymentGateway.Controllers;
using PaymentGateway.Models;
using PaymentGateway.Services;
using System;

namespace PaymentGateway.Tests.Controllers
{
    [TestClass]
    public class PaymentsController_Tests
    {
        private IBankService _bankService;
        private PaymentsController _paymentsController;

        [TestInitialize]
        public void Initialise()
        {
            var logger = Substitute.For<ILogger>();
            _bankService = Substitute.For<IBankService>();

            _paymentsController = new PaymentsController(_bankService, logger);
        }

        [TestMethod]
        public void Constructor_BankServiceNotNull()
        {
            // Arrange
            var logger = Substitute.For<ILogger>();

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new PaymentsController(null, logger));
        }

        [TestMethod]
        public void Post_NoValue()
        {
            // Arrange
            var request = new PaymentRequest("12345789", DateTime.Now.AddYears(1), 100, "GBP", 123);

            // Act 
            var result = _paymentsController.Post(null);

            // Assert
            Assert.AreEqual(500, result.StatusCode, "The result should be as expected");
        }

        [TestMethod]
        public void Post_SuccessfulPost()
        {
            // Arrange
            var request = new PaymentRequest("12345789", DateTime.Now.AddYears(1), 100, "GBP", 123);
            _bankService.Post(Arg.Any<IPaymentRequest>()).Returns(new BankPostResponse() { IsSuccess = true });

            // Act 
            var result = _paymentsController.Post(request);

            // Assert
            Assert.AreEqual(202, result.StatusCode, "The result should be as expected");
        }

        [TestMethod]
        public void Post_UnsuccessfulPost()
        {
            // Arrange
            var request = new PaymentRequest("12345789", DateTime.Now.AddYears(1), 100, "GBP", 123);
            _bankService.Post(Arg.Any<IPaymentRequest>()).Returns(new BankPostResponse() { IsSuccess = false });

            // Act 
            var result = _paymentsController.Post(request);

            // Assert
            Assert.AreEqual(500, result.StatusCode, "The result should be as expected");
        }
    }
}
