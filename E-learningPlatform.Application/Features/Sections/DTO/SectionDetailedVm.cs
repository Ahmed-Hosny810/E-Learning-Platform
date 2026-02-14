using E_learningPlatform.Application.Features.Lessons.DTO;
using E_learningPlatform.Application.Features.Modules.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Sections.DTO
{
    public class SectionDetailedVm: SectionVm
    {
        public string Description { get; set; }
        public List<LessonVm> Lessons { get; set; } = new();
    }
}
