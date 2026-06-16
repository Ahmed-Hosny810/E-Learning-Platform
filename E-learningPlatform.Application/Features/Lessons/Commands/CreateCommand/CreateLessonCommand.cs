using AutoMapper;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Lessons.Commands.CreateCommand
{
    public class CreateLessonCommand:IRequest<Response<int>>
    {
        public int SectionId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public int? DurationMinutes { get; set; }
        public bool IsFree { get; set; }
    }
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, Response<int>>
    {
        private readonly ILessonRepositoryAsync _lessonRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public CreateLessonCommandHandler(ILessonRepositoryAsync lessonRepository,IMapper mapper,INotificationService notificationService,IUserService userService)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
            _notificationService = notificationService;
            _userService = userService;
        }
        public async Task<Response<int>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson=_mapper.Map<Lesson>(request);
            lesson.IsPublished = true;
            
            await _lessonRepository.AddAsync(lesson);
           await  _notificationService.NotifyNewLessonAddedAsync(
                _userService.UserId,
                lesson.Section.Course.Title,
                lesson.Title,
                lesson.Id
                );

            return new Response<int>(lesson.Id);
        }
    }
}
