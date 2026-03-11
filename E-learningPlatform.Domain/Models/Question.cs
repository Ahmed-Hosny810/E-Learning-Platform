using E_learningPlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class Question:BaseEntity
    {
        public int QuizId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int Points { get; set; }
        public int OrderIndex { get; set; }
        public string Explanation { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<QuestionOption> QuestionOptions { get; set; } = new HashSet<QuestionOption>();
    }
}
