using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Models;
using PaymentGateway.Services;
using System;

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IBankService _bankService;
        private readonly ILogger _logger;

        public PaymentsController(
            IBankService bankService,
            ILogger logger)
        {
            _logger = logger;
            _logger?.LogTrace($"Creating a {nameof(PaymentsController)}");

            _bankService = bankService ?? throw new ArgumentNullException(nameof(bankService));
        }

        // GET api/payments/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            _logger?.BeginScope(id);
            _logger?.Log(LogLevel.Debug, $"Request for paymnet {id} received.");

            return "value";
        }

        // POST api/payments
        [HttpPost]
        public StatusCodeResult Post([FromBody] PaymentRequest request)
        {
            try
            {
                _logger?.BeginScope(request.Id);
                _logger?.Log(LogLevel.Debug, $"Payment request {request.Id} received.");

                // Check request, if invalid than return
                if (request == null)
                {
                    _logger?.Log(LogLevel.Debug, $"Bad payment request {request.Id}");
                    StatusCode(400);
                }

                var response = _bankService.Post(request).GetAwaiter().GetResult();

                return response.IsSuccess ? StatusCode(202) : StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger?.Log(LogLevel.Error, ex, "An unexpected exception occurred");
                return StatusCode(500);
            }
        }
    }
}
