using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace ShoesStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripePaymentController : ControllerBase
    {
        [HttpPost]
        public IActionResult Processing([FromBody]StripePaymentRequest paymentRequest)
        {
            var myCharge = new ChargeCreateOptions();
            myCharge.Source = paymentRequest.tokenId;
            myCharge.Amount = paymentRequest.amount;
            myCharge.Currency = "vnd";
            myCharge.Metadata = new Dictionary<string, string>();
            myCharge.Metadata["OurRef"] = "OurRef-" + Guid.NewGuid().ToString();

            var chargeService = new ChargeService();
            Charge stripeCharge = chargeService.Create(myCharge);

            return Ok(stripeCharge);
        }

        public class StripePaymentRequest
        {
            public string tokenId { get; set; }
            public int amount { get; set; }
        }
    }
}
