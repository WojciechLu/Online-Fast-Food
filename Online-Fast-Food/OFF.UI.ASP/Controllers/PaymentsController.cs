// This example sets up an endpoint using the ASP.NET MVC framework.
// Watch this video to get started: https://youtu.be/2-mMOB8MhmE.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OFF.Domain.Common.Models.Payment;
using Stripe;
using Stripe.Checkout;

namespace Online_Fast_Food.UI.ASP.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : Controller
{
    public PaymentsController()
    {
		StripeConfiguration.ApiKey = "sk_test_51LHiyMA16oTjMyUCI8vFgW2roVUHVnFas4DLZ8tKVmhbEaKZaYjRrwm3Xkco8mSAVKItXIYq1E0MSQOxGvRHeOq400zVqmBSCi";
	}

	[HttpPost("create-checkout-session")]
    public async Task<ActionResult> CreateCheckoutSessionAsync([FromBody] CreateCheckoutSessionRequest req)
	{
		req.PriceId = "price_1LJuAtA16oTjMyUCqtnZnGA1";
		var options = new SessionCreateOptions
		{
			SuccessUrl = "http://localhost:4200/success",
			CancelUrl = "http://localhost:4200/failure",
			PaymentMethodTypes = new List<string>
				{
					"card",
				},
			Mode = "subscription",
			LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						Price = req.PriceId,
						Quantity = 1,
					},
				},
		};

		var service = new SessionService();
		service.Create(options);
		try
		{
			var session = await service.CreateAsync(options);
			return Ok(new CreateCheckoutSessionResponse
			{
				SessionId = session.Id,
			});
		}
		catch (StripeException e)
		{
			Console.WriteLine(e.StripeError.Message);
			return BadRequest(new ErrorResponse
			{
				ErrorMessage = new ErrorMessage
				{
					Message = e.StripeError.Message,
				}
			});
		}
	}

	[HttpPost("list")]
	public async Task<ActionResult> CreatePrice([FromBody] PriceDTO price)
	{
		var options = new PriceListOptions { Limit = 5 };
		var service = new PriceService();
		StripeList<Price> prices = service.List(options);
		return Ok(prices);
	}
}