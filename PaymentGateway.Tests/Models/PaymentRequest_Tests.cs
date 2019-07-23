using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Models;
using System;

namespace PaymentGateway.Tests.Models
{
    [TestClass]
    public class PaymentRequest_Tests
    {
        [TestMethod]
        public void Constructor_DefinesId_DefaultConstructor()
        {
            // Arrange
            var request = new PaymentRequest();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(request.Id), "Id should be assigned");
        }

        [TestMethod]
        public void Constructor_DefinesId_FullConstructor()
        {
            // Arrange
            var request = new PaymentRequest("132465789", DateTime.Now, 100, "GBP", 123);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(request.Id), "Id should be assigned");
        }
        [TestMethod]
        public void Constructor_RequestTime_DefaultConstructor()
        {
            // Arrange
            var currentTime = DateTime.Now;
            var request = new PaymentRequest();

            // Assert
            Assert.IsTrue(currentTime <= request.RequestTime && request.RequestTime <= DateTime.Now, 
                "Request time should be set by constructor");
        }

        [TestMethod]
        public void Constructor_RequestTime_FullConstructor()
        {
            // Arrange
            var currentTime = DateTime.Now;
            var request = new PaymentRequest("132465789", DateTime.Now, 100, "GBP", 123);

            // Assert
            Assert.IsTrue(currentTime <= request.RequestTime && request.RequestTime <= DateTime.Now,
                "Request time should be set by constructor");
        }
    }
}
