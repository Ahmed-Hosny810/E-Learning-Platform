using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Enrollments.DTO
{
    public class EnrollmentDetailedVm: EnrollmentVm
    {
        public decimal ProgressPercentage { get; set; }
        public CourseVm? CourseVm { get; set; }
        //public ICollection<Payment> Payments { get; set; }
    }
}
