using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Repositories
{
    public interface ILessonProgressRepositoryAsync: IGenericRepositoryAsync<LessonProgress,int>
    {
    }
}
