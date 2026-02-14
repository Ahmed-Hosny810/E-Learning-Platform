using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Lessons.Commands.DeleteCommand
{
    public class DeleteLessonCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, Response<int>>
    {
        private readonly ILessonRepositoryAsync _lessonRepository;

        public DeleteLessonCommandHandler(ILessonRepositoryAsync lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        public async Task<Response<int>> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetByIdAsync(request.Id);
            if (lesson == null) throw new ApiException("Lesson not found");
            await _lessonRepository.DeleteAsync(lesson);
            return new Response<int>(request.Id);
        }
    }
}
