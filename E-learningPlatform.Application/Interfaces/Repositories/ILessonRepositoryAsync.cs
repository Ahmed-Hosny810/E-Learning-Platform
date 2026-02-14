using E_learningPlatform.Application.Features.Lessons.Queries.GetAllQuery;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Repositories
{
    public interface ILessonRepositoryAsync: IGenericRepositoryAsync<Lesson,int>
    {
        Task<PagedResponse<IEnumerable<Lesson>>> GetLessonsPagedResponseAsync(LessonFilter filter, LessonIncludes includes,
            LessonOrderKey orderKey, bool orderDescending, int pageNumber, int pageSize);

        Task<Lesson> GetLessonByIdAsync(int id, LessonIncludes includes);
    }
}
