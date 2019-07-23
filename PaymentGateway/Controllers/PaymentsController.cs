using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Models;
using PaymentGateway.Repository;
using PaymentGateway.Services;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public class PaymentsController : Controller
    {
        private readonly IPaymentRequestHandler _paymentHandler;
        private readonly IPaymentRequestRepository _repository;
        private readonly ILogger _logger;

        public PaymentsController(
            IPaymentRequestHandler paymentHandler,
            IPaymentRequestRepository repository,
            ILogger logger)
        {
            _logger = logger;
            _logger?.LogTrace($"Creating a {nameof(PaymentsController)}");

            _paymentHandler = paymentHandler ?? throw new ArgumentNullException(nameof(paymentHandler));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET api/payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SecuredPaymentRequest>> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _logger?.LogDebug($"Bad payment request '{id}'");
                    return BadRequest("Id Required");
                }

                _logger?.BeginScope(id);
                _logger?.LogDebug($"Request for payment '{id}' received.");

                var request = _repository.Read(id);

                if (request == null)
                    return NotFound();

                return Json(request);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An unexpected exception occurred");
                return StatusCode(500);
            }
        }

        // POST api/payments
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentRequest request)
        {
            try
            {
                // Check request, if invalid than return
                if (request == null)
                {
                    _logger?.LogDebug($"Bad payment request {request.Id}");
                    return BadRequest("Payment request required");
                }

                _logger?.BeginScope(request.Id);
                _logger?.LogDebug($"Payment request {request.Id} received.");

                var id = await _paymentHandler.HandleRequest(request);

                // Generate uri for get request
                if (Request == null)
                    return Created("", request);

                var uri = new UriBuilder
                {
                    Host = Request.Host.Host ,
                    Path = Request.Path + "/" + id,
                    Port = Request.Host.Port ?? 443,
                    Scheme = Uri.UriSchemeHttps,
                };
                return Created(uri.Uri, request);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An unexpected exception occurred");
                return BadRequest();
            }
        }
    }
}
