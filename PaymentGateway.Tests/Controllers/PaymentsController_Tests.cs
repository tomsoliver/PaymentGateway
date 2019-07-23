using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PaymentGateway.Controllers;
using PaymentGateway.Models;
using PaymentGateway.Repository;
using PaymentGateway.Services;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Tests.Controllers
{
    [TestClass]
    public class PaymentsController_Tests
    {
        private IPaymentRequestHandler _handler;
        private IPaymentRequestRepository _respository;
        private PaymentsController _paymentsController;

        [TestInitialize]
        public void Initialise()
        {
            var logger = Substitute.For<ILogger>();
            _handler = Substitute.For<IPaymentRequestHandler>();
            _respository = Substitute.For<IPaymentRequestRepository>();

            _paymentsController = new PaymentsController(_handler, _respository, logger);
        }

        [TestMethod]
        public void Constructor_BankServiceNotNull()
        {
            // Arrange
            var logger = Substitute.For<ILogger>();

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new PaymentsController(null, _respository, logger),
                $"Should throw an exception for null {nameof(IPaymentRequestHandler)}");
            Assert.ThrowsException<ArgumentNullException>(() => new PaymentsController(_handler, null, logger),
                $"Should throw an exception for null {nameof(IPaymentRequestRepository)}");
        }

        [TestMethod]
        public void Get_NoValue()
        {
            // Act - Null
            var actionsResult = _paymentsController.Get(null).GetAwaiter().GetResult();
            var result = actionsResult.Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result, "The result should be as expected");
        }

        [TestMethod]
        public void Get_Successful()
        {
            // Arrange
            var request = new PaymentRequest("123456789", DateTime.Now.AddYears(1), 100, "GBP", 123);
            var securedRequest = new SecuredPaymentRequest(request);
            _respository.Read(request.Id).Returns(securedRequest);

            // Act - Null
            var actionsResult = _paymentsController.Get(request.Id).GetAwaiter().GetResult();
            var result = actionsResult.Result as JsonResult;

            // Verify value
            var returnedRequest = result.Value as SecuredPaymentRequest;
            Assert.AreEqual(securedRequest.Id, returnedRequest.Id, "The id should be as expected");
            Assert.AreEqual(securedRequest.CardNumber, returnedRequest.CardNumber, "The CardNumber should be as expected");
            Assert.AreEqual(securedRequest.CurrencyCode, returnedRequest.CurrencyCode, "The CurrencyCode should be as expected");
            Assert.AreEqual(securedRequest.Cvv, returnedRequest.Cvv, "The Cvv should be as expected");
            Assert.AreEqual(securedRequest.ExpiryDate, returnedRequest.ExpiryDate, "The ExpiryDate should be as expected");
            Assert.AreEqual(securedRequest.Amount, returnedRequest.Amount, "The Amount should be as expected");
        }

        [TestMethod]
        public void Get_Unsucessful()
        {
            var request = new PaymentRequest("123456789", DateTime.Now.AddYears(1), 100, "GBP", 123);
            var securedRequest = new SecuredPaymentRequest(request);
            _respository.Read(request.Id).Returns(s => throw new Exception());

            // Act - Null
            var actionsResult = _paymentsController.Get(request.Id).GetAwaiter().GetResult();
            var result = actionsResult.Result as StatusCodeResult;

            // Assert
            Assert.AreEqual(500, result.StatusCode, "The result should be as expected");
        }

        [TestMethod]
        public void Post_NoValue()
        {
            // Act 
            var actionsResult = _paymentsController.Post(null).GetAwaiter().GetResult();
            var result = actionsResult as StatusCodeResult;

            // Assert
            Assert.AreEqual(400, result.StatusCode, "The result should be as expected");
        }

        [TestMethod]
        public void Post_SuccessfulPost()
        {
            // Arrange
            var request = new PaymentRequest("123456789", DateTime.Now.AddYears(1), 100, "GBP", 123);
            _handler.HandleRequest(Arg.Any<IPaymentRequest>()).Returns(Task.Run(() => { return request.Id; }));

            // Act 
            var actionsResult = _paymentsController.Post(request).GetAwaiter().GetResult();
            var result = actionsResult as CreatedResult;

            // Assert
            Assert.AreEqual(201, result.StatusCode, "The result should be as expected");
        }

        [TestMethod]
        public void Post_UnsuccessfulPost()
        {
            // Arrange
            var request = new PaymentRequest("123456789", DateTime.Now.AddYears(1), 100, "GBP", 123);
            _handler.HandleRequest(Arg.Any<IPaymentRequest>()).Returns(s => Task.Run(() => throw new Exception()));

            // Act 
            var actionsResult = _paymentsController.Post(request).GetAwaiter().GetResult();
            var result = actionsResult as StatusCodeResult;

            // Assert
            Assert.AreEqual(400, result.StatusCode, "The result should be as expected");
        }
    }
}
