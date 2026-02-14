using AutoMapper;
using E_learningPlatform.Application.Helpers;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Mappings;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Enums;
using E_learningPlatform.Domain.Models;
using MediatR;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Courses.Commands.CreateCourse
{
    public partial class CreateCourseCommand:IRequest<Response<int>>
    {
        public string TeacherId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public CourseLevel Level { get; set; } 
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
        public string? Requirements { get; set; }
        public string? WhatYouWillLearn { get; set; }
    }
    public partial class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Response<int>>
    {
        private readonly ICourseRepositoryAsync _courseRepository;
        private readonly IMapper _mapper;

        public CreateCourseCommandHandler(ICourseRepositoryAsync courseRepository,IMapper mapper)
        {
           _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course=_mapper.Map<Course>(request);
            //course.TeacherId = _currentUserService.UserId;
            course.Slug = SlugHelper.Generate(request.Title);
            course.IsPublished = false;
            await _courseRepository.AddAsync(course);
            return new Response<int>(course.Id);
        }
    }
}
