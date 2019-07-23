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
            var request = new PaymentRequest("123456789", DateTime.Now, 100, "GPB", 123);
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
            var request = new PaymentRequest("123456789", DateTime.Now, 100, "GPB", 123);
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
            var request = new PaymentRequest("123456789", DateTime.Now, 100, "GPB", 123);
            var repository = GetRepository(new List<IPaymentRequest>() { request });

            // Act 
            var result = repository.Read(request.Id);

            // Assert
            // Compare request and result
            Assert.AreEqual(request.Id, result.Id, "The Id should be the same");
            Assert.AreEqual(request.Amount, result.Amount, "The Amount should be the same");
            Assert.AreEqual(request.ExpiryDate, result.ExpiryDate, "The ExpiryDate should be the same");
            Assert.AreEqual(request.CurrencyCode, result.CurrencyCode, "The CurrencyCode should be the same");

            Assert.AreNotEqual(request.CardNumber, result.CardNumber, "The CardNumber should NOT be the same");
            Assert.AreNotEqual(request.Cvv, result.Cvv, "The Cvv should NOT be the same");
        }

        [TestMethod]
        public void Update_ArgumentValidation()
        {
            // Arrange
            var repository = GetRepository(new List<IPaymentRequest>());

            // Act && Assert
            Assert.ThrowsException<ArgumentException>(() => repository.UpdateResult(null, true),
                "Should throw an exception for null payment request");
            Assert.ThrowsException<ArgumentException>(() => repository.UpdateResult("", true),
                "Should throw an exception for empty payment request");
        }

        [TestMethod]
        public void Update()
        {
            // Arrange
            var request = new PaymentRequest("123456789", DateTime.Now, 100, "GPB", 123);
            var repository = GetRepository(new List<IPaymentRequest>() { request });

            // Act 
            var result = repository.UpdateResult(request.Id, true);
            var readResult = repository.Read(request.Id);

            // Assert
            // Compare request and result
            Assert.AreEqual(readResult.Result, true, "The result should be true");
        }

        [TestMethod]
        public void CreateAndRead()
        {
            // Arrange
            var request = new PaymentRequest("123456789", DateTime.Now, 100, "GPB", 123);
            var repository = GetRepository(new List<IPaymentRequest>());

            // Act 
            var createResult = repository.Create(request);
            var readResult = repository.Read(request.Id);

            // Assert
            Assert.AreEqual(request.Id, createResult, "The expected request should be returned when created");

            // Compare request and result
            Assert.AreEqual(request.Id, readResult.Id, "The Id should be the same");
            Assert.AreEqual(request.Amount, readResult.Amount, "The Amount should be the same");
            Assert.AreEqual(request.ExpiryDate, readResult.ExpiryDate, "The ExpiryDate should be the same");
            Assert.AreEqual(request.CurrencyCode, readResult.CurrencyCode, "The CurrencyCode should be the same");

            Assert.AreNotEqual(request.CardNumber, readResult.CardNumber, "The CardNumber should NOT be the same");
            Assert.AreNotEqual(request.Cvv, readResult.Cvv, "The Cvv should NOT be the same");
        }
    }
}
