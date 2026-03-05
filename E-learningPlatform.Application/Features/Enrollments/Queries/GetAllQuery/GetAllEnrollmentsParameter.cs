using E_learningPlatform.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Enrollments.Queries.GetAllQuery
{
    public class GetAllEnrollmentsParameter: RequestParameter<EnrollmentOrderKey>
    {
        public EnrollmentFilter? Filter { get; set; }
        public EnrollmentIncludes? Includes { get; set; }
    }

    public class EnrollmentFilter
    {
        public string? UserId { get; set; }
        public int? CourseId { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsActive { get; set; }
    }
    public class EnrollmentIncludes
    {
        public bool Course { get; set; }
        public bool Payments { get; set; }
    }
    public enum EnrollmentOrderKey
    {
        CourseId,
        EnrolledAt,
        CompletedAt,
        PurchasePrice,
        CreatedAt
    }
}
