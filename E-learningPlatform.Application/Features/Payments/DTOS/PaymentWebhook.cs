using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Payments.DTOS
{
    public class PaymentWebhook
    {
        public int EnrollmentId { get; set; }
        public string ProviderTransactionId { get; set; } 
        public decimal AmountPaid { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; } 
    }
}
