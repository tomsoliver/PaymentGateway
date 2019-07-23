using Microsoft.Extensions.Logging;
using PaymentGateway.Models;
using System;
using System.Collections.Concurrent;

namespace PaymentGateway.Repository
{
    /// <summary>
    /// An in memory implementation of a payment repository
    /// </summary>
    public class InMemoryRepository : IPaymentRequestRepository
    {
        private readonly ILogger _logger;
        private ConcurrentDictionary<string, SecuredPaymentRequest> _inMemoryStore = 
            new ConcurrentDictionary<string, SecuredPaymentRequest>();

        /// <summary>
        /// Create an in memory repository
        /// </summary>
        /// <param name="logger"></param>
        public InMemoryRepository(ILogger logger)
        {
            _logger = logger;
            _logger?.LogTrace($"Creating a {nameof(InMemoryRepository)}");
        }

        /// <summary>
        /// Create a payment request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns the id of th created payment</returns>
        public string Create(IPaymentRequest request)
        {
            _logger?.LogTrace("Writing request to in-memory store");

            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.Id))
                throw new ArgumentException("The request must have an Id", nameof(request.Id));

            // Check if repository contains key
            if (_inMemoryStore.ContainsKey(request.Id))
                throw new Exception($"Payment request with id '{request.Id}' already exists in repository");

            var repositoryRequest = new SecuredPaymentRequest(request);
            var result = _inMemoryStore.GetOrAdd(request.Id, repositoryRequest);

            // TODO: Create exception type for this exception
            // TODO: Add check to verify if operation was get or add, i.e. compare request and result to ensure
            // they are the same

            _logger?.LogTrace("Writen request to in-memory store");

            return result.Id;
        }

        /// <summary>
        /// Read the desired payment from the repository
        /// </summary>
        /// <param name="id">The id of the desired payment</param>
        /// <returns>Returns null if the request does not exist, or the payment request
        /// if it does</returns>
        public SecuredPaymentRequest Read(string id)
        {
            _logger?.LogTrace("Reading request from in-memory store");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("message", nameof(id));

            if (!_inMemoryStore.ContainsKey(id))
                return null;

            var request = _inMemoryStore[id];

            _logger?.LogTrace("Read request from in-memory store");
            return request;
        }

        /// <summary>
        /// Update the state of the desired payment in the repository
        /// </summary>
        /// <param name="id">The id of the desired payment</param>
        /// <returns>Returns the id of the updated payment</returns>
        public string UpdateResult(string id, bool result)
        {
            _logger?.LogTrace("Reading request from in-memory store");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("message", nameof(id));

            if (!_inMemoryStore.ContainsKey(id))
                return null;

            // TODO: Handle race conditions
            _inMemoryStore[id].Result = result;

            _logger?.LogTrace("Read request from in-memory store");
            return id;
        }
    }
}
