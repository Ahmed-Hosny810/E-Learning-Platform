using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Questions.DTO
{
    public class QuestionDto
    {
        public string QuestionText { get; set; } = null!;
        public string QuestionType { get; set; } = null!;
        public int Points { get; set; }
        public int OrderIndex { get; set; }
        public string Explanation { get; set; } = null!;

        public List<QuestionOptionDto> Options { get; set; } = new();
    }
}
