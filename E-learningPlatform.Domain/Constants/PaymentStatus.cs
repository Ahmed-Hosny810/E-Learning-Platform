using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Constants
{
    public static class PaymentStatus
    {
        public const string Pending = "Pending";
        public const string TimedOut = "TimedOut";
        public const string Succeeded = "Succeeded";
        public const string Failed = "Failed";
    }
}
