using E_learningPlatform.Application.Features.Enrollments.Queries.GetAllQuery;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Repositories
{
    public interface IEnrollmentRepositoryAsync: IGenericRepositoryAsync<Enrollment,int>
    {
        Task<PagedResponse<IEnumerable<Enrollment>>> GetEnrollmentsPagedResponseAsync(EnrollmentFilter filter, EnrollmentIncludes includes,
            EnrollmentOrderKey orderKey, bool orderDescending, int pageNumber, int pageSize);
        Task<Enrollment> GetEnrollmentByIdAsync(int id, EnrollmentIncludes includes);
        Task<bool> IsUserEnrolled(string userId, int courseId);

    }
}
