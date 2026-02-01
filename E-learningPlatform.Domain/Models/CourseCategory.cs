using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class CourseCategory
    {
        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        public DateTime AssignedAt { get; set; }

        // Navigation
        public virtual Course Course { get; set; }
        public virtual Category Category { get; set; }
    }
}
