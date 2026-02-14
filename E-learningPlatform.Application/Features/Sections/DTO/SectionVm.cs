using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Modules.DTO
{
    public class SectionVm
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        public int? DurationMinutes { get; set; }
        public bool IsPublished { get; set; }
       
    }
}
