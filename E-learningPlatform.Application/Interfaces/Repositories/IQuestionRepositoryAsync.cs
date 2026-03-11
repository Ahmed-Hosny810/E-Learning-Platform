using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Repositories
{
    public interface IQuestionRepositoryAsync:IGenericRepositoryAsync<Question,int>
    {
        Task<Question> GetQuestionWithDetailsAsync(int id);
        //Task<PagedResponse<IEnumerable<Question>>> GetQuestionsByQuizIdAsync(int quizId);
    }
}
