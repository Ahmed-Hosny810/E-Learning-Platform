using AutoMapper;
using E_learningPlatform.Application.Features.Categories.Commands.CreateCategory;
using E_learningPlatform.Application.Features.Categories.DTO;
using E_learningPlatform.Application.Features.CourseCategories.Commands.CreateCourseCategory;
using E_learningPlatform.Application.Features.CourseCategories.DTO;
using E_learningPlatform.Application.Features.Courses.Commands.CreateCourse;
using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Features.LessonContents.Commands.CreateCommand;
using E_learningPlatform.Application.Features.LessonContents.DTO;
using E_learningPlatform.Application.Features.Lessons.Commands.CreateCommand;
using E_learningPlatform.Application.Features.Lessons.DTO;
using E_learningPlatform.Application.Features.Modules.Commands.CreateCommand;
using E_learningPlatform.Application.Features.Modules.DTO;
using E_learningPlatform.Application.Features.Sections.DTO;
using E_learningPlatform.Application.Features.UserProfiles.Commands.CreateProfile;
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
            CreateMap<UserProfile, CreateProfileCommand>().ReverseMap();
            CreateMap<Course, CreateCourseCommand>().ReverseMap();
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<CourseCategory, CreateCourseCategoryCommand>().ReverseMap();
            CreateMap<Section, CreateSectionCommand>().ReverseMap();
            CreateMap<Section, SectionVm>().ReverseMap();
            CreateMap<Section, SectionDetailedVm>().ReverseMap();
            CreateMap<Lesson,CreateLessonCommand>().ReverseMap();
            CreateMap<Lesson,LessonVm>().ReverseMap();
            CreateMap<Lesson,LessonDetailedVm>().ReverseMap();
            CreateMap<LessonContent,CreateLessonContentCommand>().ReverseMap();
            CreateMap<LessonContent, LessonContentVm>()
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.ContentType.ToString()));
            //CreateMap<>().ReverseMap();
            //CreateMap<>().ReverseMap();
            //CreateMap<>().ReverseMap();
            //CreateMap<>().ReverseMap();

        }
    }
}
