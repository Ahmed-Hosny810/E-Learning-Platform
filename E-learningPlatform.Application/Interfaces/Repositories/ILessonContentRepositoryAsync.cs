using E_learningPlatform.Application.Features.LessonContents.Queries.GetAllQuery;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Repositories
{
    public interface ILessonContentRepositoryAsync:IGenericRepositoryAsync<LessonContent,int>
    {
        Task<int> GetMaxOrderByLessonId(int lessonId);
        Task<LessonContent> GetLessonContentByIdAsync(int id, LessonContentIncludes includes);
    }
}
