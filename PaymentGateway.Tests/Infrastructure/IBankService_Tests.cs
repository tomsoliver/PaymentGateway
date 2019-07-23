using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Infrastructure;
using PaymentGateway.Models;
using System;

namespace PaymentGateway.Tests.Services
{
    public abstract class IBankService_Tests
    {
        protected abstract IBankService Get();

        [TestMethod]
        public void Post_SuccessfulPost()
        {
            // Arrange
            // Set up bank service
            var bankService = Get();

            // Set up payment request
            var request = new PaymentRequest("123456789", DateTime.Now.AddYears(1), 100, "GBP", 123);

            // Act
            bankService.Post(request).GetAwaiter().GetResult();
        }
    }
}
