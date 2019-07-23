using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Models;
using PaymentGateway.Services;
using System;

namespace PaymentGateway.Tests.Services.PaymentRequestValidation
{
    [TestClass]
    public class PaymentRequestValidator_Tests : IPaymentRequestValidator_Tests
    {
        private IPaymentRequestValidator _validator;
        private PaymentRequest _payment;

        protected override IPaymentRequestValidator GetValidator()
        {
            return new PaymentRequestValidator();
        }

        [TestInitialize]
        public void Initialise()
        {
            _validator = GetValidator();
            _payment = new PaymentRequest("1234567891234567", DateTime.Now.AddYears(1), 10.51M, "GBP", 123);
        }

        [TestMethod]
        public void Validate_RequestIsValid()
        {
            // Arrange

            // Act
            var result = _validator.Validate(_payment, out var reasons);

            // Assert
            Assert.IsTrue(result, "Payment should be valid");
            Assert.IsTrue(reasons.Count == 0, "A reason should be provided");
        }

        [TestMethod]
        public void Validate_CardNumberNull()
        {
            // Arrange
            _payment.CardNumber = null;

            // Act
            var result = _validator.Validate(_payment, out var reasons);

            // Assert
            Assert.IsFalse(result, "Payment should be invalid");
            Assert.IsTrue(reasons.Count >= 1, "A reason should be provided");
        }

        [TestMethod]
        public void Validate_ExpiryDateInPast()
        {
            // Arrange
            _payment.ExpiryDate = DateTime.Now.AddYears(-1);

            // Act
            var result = _validator.Validate(_payment, out var reasons);

            // Assert
            Assert.IsFalse(result, "Payment should be invalid");
            Assert.IsTrue(reasons.Count >= 1, "A reason should be provided");
        }

        [TestMethod]
        public void Validate_PaymentValueLessThan0()
        {
            // Arrange
            _payment.Amount = 0;

            // Act
            var result = _validator.Validate(_payment, out var reasons);

            // Assert
            Assert.IsFalse(result, "Payment should be invalid");
            Assert.IsTrue(reasons.Count >= 1, "A reason should be provided");

            // Arrange - Try again with negative
            _payment.Amount = -10;

            // Act
            var result1 = _validator.Validate(_payment, out var reasons1);

            // Assert
            Assert.IsFalse(result1, "Payment should be invalid");
            Assert.IsTrue(reasons1.Count >= 1, "A reason should be provided");
        }

        [TestMethod]
        public void Validate_IncorrectCurrency()
        {
            // Arrange
            _payment.CurrencyCode = null;

            // Act
            var result = _validator.Validate(_payment, out var reasons);

            // Assert
            Assert.IsFalse(result, "Payment should be invalid");
            Assert.IsTrue(reasons.Count >= 1, "A reason should be provided");

            // Arrange - Try again with empty string
            _payment.CurrencyCode = "";

            // Act
            var result1 = _validator.Validate(_payment, out var reasons1);

            // Assert
            Assert.IsFalse(result1, "Payment should be invalid");
            Assert.IsTrue(reasons1.Count >= 1, "A reason should be provided");
        }

        [TestMethod]
        public void Validate_IncorrectCVV()
        {
            // Arrange
            _payment.Cvv = 12;

            // Act
            var result = _validator.Validate(_payment, out var reasons);

            // Assert
            Assert.IsFalse(result, "Payment should be invalid");
            Assert.IsTrue(reasons.Count >= 1, "A reason should be provided");

            // Arrange - Try again with empty string
            _payment.Cvv = 12345;

            // Act
            var result1 = _validator.Validate(_payment, out var reasons1);

            // Assert
            Assert.IsFalse(result1, "Payment should be invalid");
            Assert.IsTrue(reasons1.Count >= 1, "A reason should be provided");
        }
    }
}
