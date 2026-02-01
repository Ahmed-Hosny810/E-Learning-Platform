using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Repositories
{
    public interface ICourseCategoryRepositoryAsync:IGenericRepositoryAsync<CourseCategory,int>
    {
        Task<CourseCategory?> GetByIdsAsync(int courseId, int categoryId);
        Task<bool> ExistsAsync(int courseId, int categoryId);
    }
}
