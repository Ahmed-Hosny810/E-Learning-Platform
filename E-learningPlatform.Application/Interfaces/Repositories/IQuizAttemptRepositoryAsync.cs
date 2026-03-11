using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Repositories
{
    public interface IQuizAttemptRepositoryAsync:IGenericRepositoryAsync<QuizAttempt,int>
    {
        Task<QuizAttempt?> GetActiveAttemptAsync(string studentId, int quizId);
        Task<int> GetAttemptCountAsync(string studentId, int quizId);
        Task<QuizAttempt?> GetAttemptWithQuizDataAsync(int attemptId);
        Task<PagedResponse<IEnumerable<QuizAttempt>>> GetAttemptsByStudentIdAsync(string studentId,int pageNumber,int pageSize);
    }
}
