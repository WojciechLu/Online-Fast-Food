using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Models.Payment;
public class CreateCheckoutSessionRequest
{
	[JsonProperty("priceId")]
	public string PriceId { get; set; }
}