using System.ComponentModel.DataAnnotations;

namespace OFF.Domain.Common.Models.Payment;
public class PriceDTO
{
    [Required] public int Price { get; set; }
}
