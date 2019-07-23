using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Services.PaymentRequestValidation;
using System;

namespace PaymentGateway.Tests.Services.PaymentRequestValidation
{
    public abstract class IPaymentRequestValidator_Tests
    {        
        protected abstract IPaymentRequestValidator GetValidator();

        [TestMethod]
        public void Validate_ThrowsExceptionForNullRequest()
        {
            // Arrange
            var validator = GetValidator();

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => validator.Validate(null, out _));
        }
    }
}
