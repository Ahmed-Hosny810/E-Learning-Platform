using AutoMapper;
using E_learningPlatform.Application.Features.Categories.Commands.CreateCategory;
using E_learningPlatform.Application.Features.Categories.DTO;
using E_learningPlatform.Application.Features.CourseCategories.Commands.CreateCourseCategory;
using E_learningPlatform.Application.Features.CourseCategories.DTO;
using E_learningPlatform.Application.Features.Courses.Commands.CreateCourse;
using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Features.Enrollments.Commands.CreateCommand;
using E_learningPlatform.Application.Features.LessonContents.Commands.CreateCommand;
using E_learningPlatform.Application.Features.LessonContents.DTO;
using E_learningPlatform.Application.Features.Lessons.Commands.CreateCommand;
using E_learningPlatform.Application.Features.Lessons.DTO;
using E_learningPlatform.Application.Features.Modules.Commands.CreateCommand;
using E_learningPlatform.Application.Features.Modules.DTO;
using E_learningPlatform.Application.Features.Questions.Commands;
using E_learningPlatform.Application.Features.Questions.DTO;
using E_learningPlatform.Application.Features.Quizzes.Commands.CreateCommand;
using E_learningPlatform.Application.Features.Quizzes.DTO;
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
            CreateMap<Category,CategoryVm>().ReverseMap();
            CreateMap<CreateCourseCommand, Course>()
            .ForMember(dest => dest.CourseCategories, opt => opt.MapFrom(src => src.CourseCategory));

            CreateMap<CourseCategoryVm, CourseCategory>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<CourseCategory, CategorySimpleDto>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Course, CourseVm>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CourseCategories));

            CreateMap<UserProfile, CreateProfileCommand>().ReverseMap();
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            CreateMap<Section, CreateSectionCommand>().ReverseMap();
            CreateMap<Section, SectionVm>().ReverseMap();
            CreateMap<Section, SectionDetailedVm>().ReverseMap();
            CreateMap<Lesson,CreateLessonCommand>().ReverseMap();
            CreateMap<Lesson,LessonVm>().ReverseMap();
            CreateMap<Lesson,LessonDetailedVm>().ReverseMap();
            CreateMap<LessonContent,CreateLessonContentCommand>().ReverseMap();
            CreateMap<LessonContent, LessonContentVm>()
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.ContentType.ToString()));
            CreateMap<Enrollment,CreateEnrollmentCommand>().ReverseMap();
            CreateMap<CreateQuizCommand, Quiz>();

            CreateMap<QuestionDto, Question>()
                .ForMember(dest => dest.QuestionOptions, opt => opt.MapFrom(src => src.Options));
            CreateMap<QuestionOptionDto, QuestionOption>().ReverseMap();
            CreateMap<AddQuestionCommand, Question>();

            CreateMap<Quiz, QuizStudentDto>();

            CreateMap<Question, QuestionStudentDto>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.QuestionOptions));

            CreateMap<QuestionOption, OptionStudentDto>();

            CreateMap<Quiz, QuizEditorDto>();

            CreateMap<Question, QuestionEditorDto>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.QuestionOptions));

            CreateMap<QuestionOption, OptionEditorDto>();

            //CreateMap<>().ReverseMap();
            //CreateMap<>().ReverseMap();
            //CreateMap<>().ReverseMap();

        }
    }
}
