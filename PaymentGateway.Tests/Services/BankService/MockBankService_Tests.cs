using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PaymentGateway.Models;
using PaymentGateway.Services;

namespace PaymentGateway.Tests.Services
{
    [TestClass]
    public class MockBankService_Tests : IBankService_Tests
    {
        protected override IBankService GetBankService(IBankPostResponse expectedResponse)
        {
            return new MockBankService(Substitute.For<ILogger>())
            {
                Response = expectedResponse
            };
        }
    }
}
