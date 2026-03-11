using E_learningPlatform.Application.Interfaces.Repositories;
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
    public class QuizRepositoryAsync : GenericRepositoryAsync<Quiz, int>, IQuizRepositoryAsync
    {
        private readonly ApplicationDbContext _context;
        public QuizRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Quiz> GetQuizWithQuestionsAsync(int quizId)
        {
            var quiz= await _context.Quizzes.Where(q => q.Id == quizId)
                .Include(q => q.Questions)
                .ThenInclude(q => q.QuestionOptions)
                .FirstOrDefaultAsync();
            return quiz;
        }

        public async Task<bool> IsQuizRequiredAsync(int quizId)
        {
            return await _context.Quizzes
             .Where(q => q.Id == quizId)
              .Select(q => q.IsRequired)
              .FirstOrDefaultAsync();
        }
    }
}
