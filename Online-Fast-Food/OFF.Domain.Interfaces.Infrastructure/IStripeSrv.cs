using OFF.Domain.Common.Models.Dish;
using Stripe;

namespace OFF.Domain.Interfaces.Infrastructure;
public interface IStripeSrv
{
    Product CreateProduct(AddDishDTO addDishDTO);
}
