// This example sets up an endpoint using the ASP.NET MVC framework.
// Watch this video to get started: https://youtu.be/2-mMOB8MhmE.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace server.Controllers
{
    public class PaymentsController : Controller
    {
        public PaymentsController()
        {
            StripeConfiguration.ApiKey = "sk_test_51LHiyMA16oTjMyUCI8vFgW2roVUHVnFas4DLZ8tKVmhbEaKZaYjRrwm3Xkco8mSAVKItXIYq1E0MSQOxGvRHeOq400zVqmBSCi";
        }

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession()
        {
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                      UnitAmount = 2000,
                      Currency = "usd",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = "T-shirt",
                      },

                    },
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = "https://github.com/WojciechLu/Online-Fast-Food",
                CancelUrl = "https://www.youtube.com/watch?v=TlsyOhusBPE",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}