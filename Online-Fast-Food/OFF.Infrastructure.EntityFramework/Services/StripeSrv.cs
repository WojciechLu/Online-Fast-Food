using OFF.Domain.Common.Models.Dish;
using OFF.Domain.Interfaces.Infrastructure;
using Stripe;

namespace OFF.Infrastructure.EntityFramework.Services;
public class StripeSrv : IStripeSrv
{
    public StripeSrv()
    {
        StripeConfiguration.ApiKey = "sk_test_51LHiyMA16oTjMyUCI8vFgW2roVUHVnFas4DLZ8tKVmhbEaKZaYjRrwm3Xkco8mSAVKItXIYq1E0MSQOxGvRHeOq400zVqmBSCi";
    }
    public Product CreateProduct(AddDishDTO addDishDTO)
    {
        int priceInt = (int)addDishDTO.Price * 100;
        var options = new ProductCreateOptions
        {
            Name = addDishDTO.Name,
            DefaultPriceData = new ProductDefaultPriceDataOptions
            {
                UnitAmount = priceInt,
                Currency = "pln",
                Recurring = null,
                TaxBehavior = "inclusive",
                UnitAmountDecimal = priceInt

            },
            Expand = new List<string> { "default_price" },
            Type = "one_time",
            Active = true,
            Description = addDishDTO.Description,
        };
        var service = new ProductService();
        var result = service.Create(options);

        return result;
    }
}
