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
    public class QuestionRepositoryAsync : GenericRepositoryAsync<Question, int>, IQuestionRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Question> GetQuestionWithDetailsAsync(int id)
        {
            return await _context.Questions
                 .Include(q => q.QuestionOptions)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
