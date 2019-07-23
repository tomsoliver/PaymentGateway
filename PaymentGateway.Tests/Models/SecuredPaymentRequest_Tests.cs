using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Models;
using System;

namespace PaymentGateway.Tests.Models
{
    [TestClass]
    public class SecuredPaymentRequest_Tests
    {
        [TestMethod]
        public void Constructor_SecuresCardNumber()
        {
            // Arrange
            var request = new PaymentRequest("123456789", DateTime.Now, 100, "GPB", 123);

            // Act
            var securedRequest = new SecuredPaymentRequest(request);

            // Assert
            Assert.AreEqual("*****6789", securedRequest.CardNumber, "Card number should be secured");
        }

        [TestMethod]
        public void Constructor_SecuresCvv()
        {
            // Arrange
            var request = new PaymentRequest("123456789", DateTime.Now, 100, "GPB", 123);

            // Act
            var securedRequest = new SecuredPaymentRequest(request);

            // Assert
            Assert.AreEqual("***", securedRequest.Cvv, "Cvv should be secured");
        }

        [TestMethod]
        public void Constructor_AllOtherPropertiesTheSame ()
        {
            // Arrange
            var request = new PaymentRequest("123456789", DateTime.Now, 100, "GPB", 123);

            // Act
            var securedRequest = new SecuredPaymentRequest(request);

            // Assert
            Assert.AreEqual(request.Amount, securedRequest.Amount, "Amount should be secured");
            Assert.AreEqual(request.CurrencyCode, securedRequest.CurrencyCode, "CurrencyCode should be secured");
            Assert.AreEqual(request.ExpiryDate, securedRequest.ExpiryDate, "ExpiryDate should be secured");
            Assert.AreEqual(request.Id, securedRequest.Id, "Id should be secured");
        }
    }
}
