using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Enrollments.DTO
{
    public class EnrollmentVm
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }

        public bool IsPaid { get; set; }
        public decimal PurchasePrice { get; set; }

        public DateTime EnrolledAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public bool IsActive { get; set; }

    }
}
