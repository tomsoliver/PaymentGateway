using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PaymentGateway.Infrastructure;
using PaymentGateway.Models;
using System;

namespace PaymentGateway.Tests.Services
{
    [TestClass]
    public class MockBankService_Tests : IBankService_Tests
    {
        protected override IBankService Get()
        {
            return new MockBankService(Substitute.For<ILogger>())
            {
                Success = true
            };
        }

        [TestMethod]
        public void Post_UnsuccessfulPost()
        {
            // Arrange
            // Set up bank service
            var bankService = new MockBankService(Substitute.For<ILogger>())
            {
                Success = false
            }; 

            // Set up payment request
            var request = new PaymentRequest("123456789", DateTime.Now.AddYears(1), 100, "GBP", 123);

            // Act && Assert
            Assert.ThrowsException<BankPostException>(() => bankService.Post(request).GetAwaiter().GetResult());
        }
    }
}
