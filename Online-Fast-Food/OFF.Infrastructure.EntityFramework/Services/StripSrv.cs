using OFF.Domain.Common.Models.Dish;
using OFF.Domain.Common.Models.Payment;
using OFF.Domain.Interfaces.Infrastructure;
using Stripe;
using Stripe.Checkout;

namespace OFF.Infrastructure.EntityFramework.Services;
public class StripeSrv : IStripeSrv
{
    public StripeSrv()
    {
        StripeConfiguration.ApiKey = "sk_test_51LHiyMA16oTjMyUCI8vFgW2roVUHVnFas4DLZ8tKVmhbEaKZaYjRrwm3Xkco8mSAVKItXIYq1E0MSQOxGvRHeOq400zVqmBSCi";
    }

    public async Task<CreateCheckoutSessionResponse> CreateCheckOutAsync(CreateCheckoutSessionRequest req)
    {
        var list = new List<SessionLineItemOptions>();
        foreach (var item in req.DictionaryPrice)
        {
            var i = new SessionLineItemOptions
            {
                Price = item.Key,
                Quantity = item.Value,
            };
            list.Add(i);
        }

        var options = new SessionCreateOptions
        {
            SuccessUrl = "http://localhost:2137/api/Order/completeOrder",
            CancelUrl = "http://localhost:4200/smutnaStronaOBłędzie(nie_mylic_z_wykladowca)",
            PaymentMethodTypes = new List<string>
                {
                    "card",
                },
            Mode = "payment",
            LineItems = list
        };

        var service = new SessionService();
        service.Create(options);
        var session = await service.CreateAsync(options);
        return new CreateCheckoutSessionResponse
        {
            SessionId = session.Id,
        };
    }

    public Product CreateProduct(AddDishDTO addDishDTO)
    {
        float priceFloat = addDishDTO.Price * 100;
        int priceInt = (int)priceFloat;
        var options = new ProductCreateOptions
        {
            Name = addDishDTO.Name,
            DefaultPriceData = new ProductDefaultPriceDataOptions
            {
                UnitAmount = priceInt,
                Currency = "pln",
                Recurring = null,
                TaxBehavior = "inclusive",

            },
            Expand = new List<string> { "default_price" },
            Active = true,
            Description = addDishDTO.Description,
        };
        var service = new ProductService();
        var result = service.Create(options);

        return result;
    }

    public Product GetProduct(string id)
    {
        var service = new ProductService();
        var product = service.Get(id);
        return product;
    }
}