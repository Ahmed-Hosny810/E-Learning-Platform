using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
   
        public string Title { get; set; }
        public string Slug { get; set; }

        public string Description { get; set; }
        public string? ThumbnailUrl { get; set; }

        public CourseLevel Level { get; set; }   
        public string Language { get; set; }

        public decimal Price { get; set; }
        public string Currency { get; set; }

        public string? Requirements { get; set; }
        public string? WhatYouWillLearn { get; set; }

        public bool IsPublished { get; set; }
    }

    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Response<int>>
    {
        private readonly ICourseRepositoryAsync _courseRepository;

        public UpdateCourseCommandHandler(ICourseRepositoryAsync courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Response<int>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);
            if (course==null)
            {
                throw new ApiException($"Course Not Found.");
            }

            course.Title = request.Title;
            course.Description = request.Description;
            course.Price = request.Price;
            course.Currency = request.Currency;
            course.Language = request.Language;
            course.Level = (CourseLevel)request.Level;
            course.ThumbnailUrl = request.ThumbnailUrl;
            course.Requirements = request.Requirements;
            course.WhatYouWillLearn = request.WhatYouWillLearn;
            course.IsPublished = request.IsPublished;

            if (request.IsPublished && course.PublishedAt == null)
                course.PublishedAt = DateTime.UtcNow;

            await _courseRepository.UpdateAsync(course);

            return new Response<int>(course.Id);

        }
    }
}
