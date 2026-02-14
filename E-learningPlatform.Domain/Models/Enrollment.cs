using E_learningPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public int CourseId { get; set; }

        public EnrollmentType EnrollmentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public decimal ProgressPercentage { get; set; }

        public DateTime EnrolledAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public bool IsActive { get; set; }

        public  Course? Course { get; set; }
    }
}
