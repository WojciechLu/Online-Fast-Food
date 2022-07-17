using OFF.Domain.Common.Models.Dish;
using OFF.Domain.Common.Models.Payment;
using Stripe;

namespace OFF.Domain.Interfaces.Infrastructure;
public interface IStripeSrv
{
    Product CreateProduct(AddDishDTO addDishDTO);
    Product GetProduct(string id);
    Task<CreateCheckoutSessionResponse> CreateCheckOutAsync(CreateCheckoutSessionRequest req);
}
