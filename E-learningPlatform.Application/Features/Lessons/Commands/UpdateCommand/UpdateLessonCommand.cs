using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Lessons.Commands.UpdateCommand
{
    public class UpdateLessonCommand: IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public int? DurationMinutes { get; set; }
        public bool IsFree { get; set; }
    }
    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, Response<int>>
    {
        private readonly ILessonRepositoryAsync _lessonRepository;

        public UpdateLessonCommandHandler(ILessonRepositoryAsync lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        public async Task<Response<int>> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetByIdAsync(request.Id);
            if (lesson == null) throw new ApiException("Lesson not found");
            lesson.Title = request.Title;
            lesson.Description = request.Description;
            lesson.DisplayOrder = request.DisplayOrder;
            lesson.DurationMinutes = request.DurationMinutes;
            lesson.IsFree = request.IsFree;
            await _lessonRepository.UpdateAsync(lesson);
            return new Response<int>(lesson.Id);
        }
    }
}
