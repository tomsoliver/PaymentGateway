using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Models;
using PaymentGateway.Repository;
using System;
using System.Collections.Generic;

namespace PaymentGateway.Tests.Repository
{
    public abstract class IPaymentRequestRepository_Tests
    {
        protected abstract IPaymentRequestRepository GetRepository(List<IPaymentRequest> requests);

        [TestMethod]
        public void Create_ArgumentValidation_NullRequest()
        {
            // Arrange
            var repository = GetRepository(new List<IPaymentRequest>());

            // Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => repository.Create(null),
                "Should throw an exception for null payment request");
        }

        [TestMethod]
        public void Create_ArgumentValidation_BadId()
        {
            // Should throw an exception if the incoming request has a null or empty Id
            // Arrange
            var request = new PaymentRequest() { Id = null };
            var repository = GetRepository(new List<IPaymentRequest>());

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => repository.Create(request),
                "Should throw an exception for null payment request id");

            // Test Empty Id
            // Arrange
            var request1 = new PaymentRequest() { Id = "" };
            var repository1 = GetRepository(new List<IPaymentRequest>());

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => repository1.Create(request1),
                "Should throw an exception for empty payment request id");
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            var request = new PaymentRequest("1234", DateTime.Now, 100, "GPB", 123);
            var repository = GetRepository(new List<IPaymentRequest>());

            // Act 
            var result = repository.Create(request);

            // Assert
            Assert.AreEqual(result, request.Id, "The id of the created request should be returned");
        }

        // TODO: Verify this test to see if exception should be thrown or not
        [TestMethod]
        public void Create_AlreadyAdded()
        {
            // Arrange
            var request = new PaymentRequest("1234", DateTime.Now, 100, "GPB", 123);
            var repository = GetRepository(new List<IPaymentRequest>());

            // Act 
            var result = repository.Create(request);

            // Assert
            Assert.AreEqual(result, request.Id, "The id of the created request should be returned");
            Assert.ThrowsException<Exception>(() => repository.Create(request), 
                "Should throw an exception when trying to create a pre-exisiting request");
        }

        [TestMethod]
        public void Read_ArgumentValidation()
        {
            // Arrange
            var repository = GetRepository(new List<IPaymentRequest>());

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => repository.Read(null),
                "Should throw an exception for null payment request");
            Assert.ThrowsException<ArgumentException>(() => repository.Read(""),
                "Should throw an exception for empty payment request");
        }

        [TestMethod]
        public void Read()
        {
            // Arrange
            var request = new PaymentRequest("1234", DateTime.Now, 100, "GPB", 123);
            var repository = GetRepository(new List<IPaymentRequest>() { request });

            // Act 
            var result = repository.Read(request.Id);

            // Assert
            Assert.IsTrue(request.Equals(result), "The expected request should be returned");
        }

        [TestMethod]
        public void CreateAndRead()
        {
            // Arrange
            var request = new PaymentRequest("1234", DateTime.Now, 100, "GPB", 123);
            var repository = GetRepository(new List<IPaymentRequest>());

            // Act 
            var createResult = repository.Create(request);
            var readResult = repository.Read(request.Id);

            // Assert
            Assert.AreEqual(request.Id, createResult, "The expected request should be returned when created");
            Assert.IsTrue(request.Equals(readResult), "The expected request should be returned read");
        }
    }
}
