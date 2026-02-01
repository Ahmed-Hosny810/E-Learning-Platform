using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Features.Courses.Queries.GetAllCourses;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Repositories
{
    public interface ICourseRepositoryAsync:IGenericRepositoryAsync<Course,int>
    {
        Task<PagedResponse<IEnumerable<Course>>> GetCoursesPagedResponseAsync(CourseFilter filter, CourseIncludes includes,
            CourseOrderKey orderKey, bool orderDescending, int currentPage, int pageSize);

        Task<Course> GetCourseByIdAsync(int id, CourseIncludes includes);
    }
}
