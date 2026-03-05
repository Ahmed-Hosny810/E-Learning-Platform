using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Wrappers
{
    public class PaymentSessionResponse
    {
        public string CheckoutUrl { get; set; } = string.Empty;
        public string? SessionId { get; set; }
    }
}
