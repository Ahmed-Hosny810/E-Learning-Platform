using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Questions.DTO
{
    public class QuestionOptionDto
    {
        public string OptionText { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int OrderIndex { get; set; }
    }
}
