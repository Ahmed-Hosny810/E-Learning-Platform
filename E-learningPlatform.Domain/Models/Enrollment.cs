using E_learningPlatform.Domain.Common;
using E_learningPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class Enrollment:BaseEntity
    {
        public int UserProfileId { get; set; }
        public int CourseId { get; set; }

        public bool IsPaid { get; set; }
        public decimal PurchasePrice { get; set; }

        public decimal ProgressPercentage { get; set; }

        public DateTime EnrolledAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public bool IsActive { get; set; }

        public  Course? Course { get; set; }
        public ICollection<Payment> Payments { get; set; }

        public UserProfile UserProfile { get; set; } = null!;
    }
}
