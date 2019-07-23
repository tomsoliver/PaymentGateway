using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PaymentGateway.Infrastructure;
using PaymentGateway.Models;
using PaymentGateway.Repository;
using PaymentGateway.Services;
using System;
using System.Collections.Generic;

namespace PaymentGateway.Tests.Services
{
    [TestClass]
    public class PaymentRequestHandler_Tests : IPaymentRequestHandler_Tests
    {
        protected override IPaymentRequestHandler Get(bool isRequestValid)
        {
            var repository = Substitute.For<IPaymentRequestRepository>();
            var validator = Substitute.For<IPaymentRequestValidator>();
            var bankService = Substitute.For<IBankService>();
            var logger = Substitute.For<ILogger>();

            if (isRequestValid)
                validator.Validate(Arg.Any<IPaymentRequest>(), out Arg.Any<IReadOnlyCollection<string>>()).Returns(true);

            return new PaymentRequestHandler(repository, validator, bankService, logger);
        }

        [TestMethod]
        public void Handle_CalledInOrder()
        {
            // Service must create request in repository, validate, and then post.
            // Arrange
            var repository = Substitute.For<IPaymentRequestRepository>();
            var validator = Substitute.For<IPaymentRequestValidator>();
            var bankService = Substitute.For<IBankService>();
            var logger = Substitute.For<ILogger>();
            var handler = new PaymentRequestHandler(repository, validator, bankService, logger);

            // Set up request and mark as valid
            validator.Validate(Arg.Any<IPaymentRequest>(), out Arg.Any<IReadOnlyCollection<string>>()).Returns(true);
            var request = new PaymentRequest("123456789", DateTime.Now, 100, "GPB", 123);

            // Act
            var result = handler.HandleRequest(request).GetAwaiter().GetResult();

            // Assert
            Received.InOrder(async () =>
            {
                repository.Create(request);
                validator.Validate(request, out _);
                await bankService.Post(request);
            });
        }
    }
}
