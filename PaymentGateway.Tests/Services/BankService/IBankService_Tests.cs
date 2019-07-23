using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Models;
using PaymentGateway.Services;
using System;

namespace PaymentGateway.Tests.Services
{
    public abstract class IBankService_Tests
    {
        protected abstract IBankService GetBankService(IBankPostResponse expectedResponse);

        [TestMethod]
        public void Post_SuccessfulPost()
        {
            // Arrange
            // Set up bank service
            var expected = new BankPostResponse()
            {
                IsSuccess = true,
                StatusCode = 200,
                ReasonPhrase = ""
            };
            var bankService = GetBankService(expected);

            // Set up payment request
            var request = new PaymentRequest("1234", DateTime.Now.AddYears(1), 100, "GBP", 123);

            // Act
            var result = bankService.Post(request).GetAwaiter().GetResult();

            // Assert
            Assert.AreEqual(expected.IsSuccess, result.IsSuccess, "SuccessCode should be the same");
            Assert.AreEqual(expected.ReasonPhrase, result.ReasonPhrase, "ReasonPhrase should be the same");
            Assert.AreEqual(expected.StatusCode, result.StatusCode, "StatusCode should be the same");
        }

        [TestMethod]
        public void Post_UnsuccessfulPost()
        {
            // Arrange
            // Set up bank service
            var expected = new BankPostResponse()
            {
                IsSuccess = false,
                StatusCode = 500,
                ReasonPhrase = "Failed to connect to bank"
            };
            var bankService = GetBankService(expected);

            // Set up payment request
            var request = new PaymentRequest("1234", DateTime.Now.AddYears(1), 100, "GBP", 123);

            // Act
            var result = bankService.Post(request).GetAwaiter().GetResult();

            // Assert
            Assert.AreEqual(expected.IsSuccess, result.IsSuccess, "SuccessCode should be the same");
            Assert.AreEqual(expected.ReasonPhrase, result.ReasonPhrase, "ReasonPhrase should be the same");
            Assert.AreEqual(expected.StatusCode, result.StatusCode, "StatusCode should be the same");
        }
    }
}
