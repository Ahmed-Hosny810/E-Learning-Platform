using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using E_learningPlatform.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.Repositories
{
    public class QuizAttemptRepositoryAsync : GenericRepositoryAsync<QuizAttempt, int>, IQuizAttemptRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public QuizAttemptRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<QuizAttempt?> GetActiveAttemptAsync(string studentId, int quizId)
        {
            return await _context.QuizAttempts
                .Where(q=>q.StudentId==studentId&&q.QuizId == quizId && q.Status == "InProgress")
                .OrderByDescending(q => q.StartedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetAttemptCountAsync(string studentId, int quizId)
        {
            return await _context.QuizAttempts
                .CountAsync(x => x.StudentId == studentId && x.QuizId == quizId); ;
        }

        public async Task<PagedResponse<IEnumerable<QuizAttempt>>> GetAttemptsByStudentIdAsync(string studentId, int pageNumber, int pageSize)
        {
            var totalCount = await _context.QuizAttempts
                .CountAsync(q => q.StudentId == studentId);

            var attempts = await _context.QuizAttempts
                .Include(q => q.Quiz) 
                .Where(q => q.StudentId == studentId)
                .OrderByDescending(q => q.StartedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<IEnumerable<QuizAttempt>>(attempts, pageNumber, pageSize, totalCount);
        }

        public async Task<QuizAttempt?> GetAttemptWithQuizDataAsync(int attemptId)
        {
            return await _context.QuizAttempts
                    .Include(a => a.Quiz)
                    .ThenInclude(q => q.Questions)
                    .ThenInclude(q => q.QuestionOptions)
                     .Include(a => a.UserAnswers)
                     .FirstOrDefaultAsync(a => a.Id == attemptId);
        }
    }
}
