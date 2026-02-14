using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.LessonContents.Commands.DeleteCommand
{
    public class DeleteLessonContentCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteLessonContentCommandHandler : IRequestHandler<DeleteLessonContentCommand, Response<int>>
    {
        private readonly ILessonContentRepositoryAsync _repository;
        private readonly IFileStorageService _fileService;

        public DeleteLessonContentCommandHandler(ILessonContentRepositoryAsync repository, IFileStorageService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<Response<int>> Handle(DeleteLessonContentCommand request, CancellationToken cancellationToken)
        {
            
            var content = await _repository.GetByIdAsync(request.Id);
            if (content == null) return new Response<int>("Content not found.");

            // 2. Capture the path before we delete the record
            var pathToFile = content.ContentUrl;

            // 3. Delete the record from the database
            await _repository.DeleteAsync(content);

            if (!string.IsNullOrEmpty(pathToFile))
            {
                _fileService.DeleteFile(pathToFile);
            }

            return new Response<int>(content.Id);
        }
    }
}
