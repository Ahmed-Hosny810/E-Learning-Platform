using E_learningPlatform.Application.Features.LessonContents.DTO;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Lessons.DTO
{
    public class LessonDetailedVm: LessonVm
    {
        public string Description { get; set; }
        public List<LessonContentVm> lessonContent { get; set; }=new List<LessonContentVm>();
    }
}
