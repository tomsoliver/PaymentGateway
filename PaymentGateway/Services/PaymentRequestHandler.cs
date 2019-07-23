using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaymentGateway.Infrastructure;
using PaymentGateway.Models;
using PaymentGateway.Repository;

namespace PaymentGateway.Services
{
    /// <summary>
    /// Handles a payment request
    /// </summary>
    public class PaymentRequestHandler : IPaymentRequestHandler
    {
        private readonly IPaymentRequestRepository _repository;
        private readonly IPaymentRequestValidator _validator;
        private readonly IBankService _bankService;
        private readonly ILogger _logger;

        public PaymentRequestHandler(
            IPaymentRequestRepository repository,
            IPaymentRequestValidator validator,
            IBankService bankService,
            ILogger logger)
        {
            _logger = logger;
            _logger?.LogTrace($"Creating a {nameof(PaymentRequestHandler)}");

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _bankService = bankService ?? throw new ArgumentNullException(nameof(bankService));
        }

        /// <summary>
        /// Handles a payment request
        /// </summary>
        /// <returns>Returns the id of the created request</returns>
        public async Task<string> HandleRequest(IPaymentRequest request)
        {
            _logger?.LogTrace($"Processing request '{request.Id}'");

            // Persist request to local repository
            var id = _repository.Create(request);

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Validate request
            if (!_validator.Validate(request, out var reasons))
                throw new InvalidDataException(reasons);

            // Post to bank
            await _bankService.Post(request);

            // Mark as successfully processed
            _repository.UpdateResult(request.Id, true);

            return id;
        }
    }
}
