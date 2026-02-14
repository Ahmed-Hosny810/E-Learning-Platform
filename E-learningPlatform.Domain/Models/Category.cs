using E_learningPlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; } = true;

        public Category? ParentCategory { get; set; }
        public  ICollection<Category> SubCategories { get; set; }
        public  ICollection<CourseCategory> CourseCategories { get; set; }

    }
}
