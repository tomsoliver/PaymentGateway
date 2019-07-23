using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PaymentGateway.Models;
using PaymentGateway.Repository;
using System.Collections.Generic;

namespace PaymentGateway.Tests.Repository
{
    [TestClass]
    public class InMemoryRepository_Tests : IPaymentRequestRepository_Tests
    {
        protected override IPaymentRequestRepository GetRepository(List<IPaymentRequest> requests)
        {
            var repository = new InMemoryRepository(Substitute.For<ILogger>());

            foreach (var request in requests) repository.Create(request);

            return repository;
        }
    }
}
