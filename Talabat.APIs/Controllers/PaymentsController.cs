using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders_Aggrigate;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{

    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentServices _paymentServices;
        private readonly ILogger _logger;
        private const string _WebhookSecret = "whsec_73c67c2ad0cc601a53c442db86ef39ae026081331367270d4ec43e69940a7ffb";

        public PaymentsController(IPaymentServices paymentServices , ILogger<PaymentsController> logger)
        {
            _paymentServices = paymentServices;
            _logger = logger;
        }



        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentServices.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null)
                return BadRequest(new ApiResponse(400, "there is a Problem at your Basket "));

            return Ok(basket);
        }


        [HttpPost("webhook")]
        public async Task<ActionResult> StripWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripEvent = EventUtility.ConstructEvent(json, Request.Headers["Strip-Signature"], _WebhookSecret);

            PaymentIntent intent;
            Order order;

            switch (stripEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = ( PaymentIntent ) stripEvent.Data.Object;
                    order = await _paymentServices.UpdatePaymentIntentToSucceededOrFailed(intent.Id, true);
                    _logger.LogInformation(" Payment is Succeeded ",order.Id,intent.Id);
                    break;
                case "payment_intent.failed":
                    intent = (PaymentIntent)stripEvent.Data.Object;
                    order = await _paymentServices.UpdatePaymentIntentToSucceededOrFailed(intent.Id, false);
                    _logger.LogInformation(" Payment is Failed ",order.Id,intent.Id);
                    break;
            }


            return new EmptyResult();
        }


    }
}
