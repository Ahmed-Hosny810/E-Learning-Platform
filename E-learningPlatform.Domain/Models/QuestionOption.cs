using E_learningPlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class QuestionOption:BaseEntity
    {
        public string OptionText { get; set; }=string.Empty;
        public bool IsCorrect { get; set; }
        public int OrderIndex { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}
