using E_learningPlatform.Domain.Common;
using E_learningPlatform.Domain.Constants;
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
        public string ProviderPaymentId { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";

        public string PaymentMethod { get; set; } 

        public string Status { get; set; } = PaymentStatus.Pending;

        public string? FailureReason { get; set; }

        public DateTime? PaidAt { get; set; }
        public  Enrollment? Enrollment { get; set; }
    }
}
