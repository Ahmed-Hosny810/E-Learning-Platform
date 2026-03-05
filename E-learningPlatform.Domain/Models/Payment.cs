using E_learningPlatform.Domain.Common;
using E_learningPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class Payment:BaseEntity
    {
        public int EnrollmentId { get; set; }
        public string ProviderPaymentId { get; set; }  // Stripe/PayPal ID
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public string? PaymentMethod { get; set; }

        public PaymentStatus Status { get; set; }  

        public  Enrollment? Enrollment { get; set; }
    }
}
