using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Models;
using PaymentGateway.Services;

namespace PaymentGateway.Tests.Services
{
    public abstract class IPaymentRequestHandler_Tests
    {
        protected abstract IPaymentRequestHandler Get(bool isRequestValid);

        [TestMethod]
        public void Handle_ValidRequest()
        {
            // Arrange
            var handler = Get(true);
            var request = new PaymentRequest();

            // Act
            handler.HandleRequest(request).GetAwaiter().GetResult();

            // Assert

        }
    }
}
