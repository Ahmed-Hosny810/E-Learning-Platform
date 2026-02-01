using AutoMapper;
using E_learningPlatform.Application.Features.Categories.DTO;
using E_learningPlatform.Application.Features.CourseCategories.DTO;
using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Features.UserProfiles.DTO;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Mappings
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            CreateMap<UserProfile,UserProfileVm>().ReverseMap();
            CreateMap<Course,CourseVm>().ReverseMap();
            CreateMap<Category,CategoryVm>().ReverseMap();
            CreateMap<CourseCategory,CourseCategoryVm>().ReverseMap();
            //CreateMap<>().ReverseMap();
            //CreateMap<>().ReverseMap();
            //CreateMap<>().ReverseMap();
            //CreateMap<>().ReverseMap();
            //CreateMap<>().ReverseMap();

        }
    }
}
