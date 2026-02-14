using E_learningPlatform.Application.Features.Courses.Queries.GetAllCourses;
using E_learningPlatform.Application.Parameters;
using E_learningPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesParameters:RequestParameter<CategoryOrderKey>
    {
        public CategoryFilter Filter { get; set; } = new();
        public CategoryIncludes Includes { get; set; } 
    }
    public class CategoryFilter
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

    }

    public class CategoryIncludes
    {
        public bool ParentCategory { get; set; }
        public bool Subcategories { get; set; }
       // public bool Courses { get; set; }
 
    }
    public enum CategoryOrderKey
    {
        Id,
        Name
    }
}
