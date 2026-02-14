using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Application.Settings;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Enums;
using E_learningPlatform.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.LessonContents.Commands.CreateCommand
{
    public class CreateLessonContentCommand : IRequest<Response<int>>
    {
        public int LessonId { get; set; }
        public LessonContentType ContentType { get; set; }
        public int DisplayOrder { get; set; }
        public string? TextContent { get; set; }
        public IFormFile? File { get; set; }
        public string? ExternalUrl { get; set; }
        public bool IsDownloadable { get; set; }
    }
    public class CreateLessonContentCommandHandler : IRequestHandler<CreateLessonContentCommand, Response<int>>
    {
        private readonly ILessonContentRepositoryAsync _repository;
        private readonly IFileStorageService _fileService;
        private readonly IMapper _mapper;
        private readonly FileUploadSettings _settings;
        public CreateLessonContentCommandHandler(ILessonContentRepositoryAsync lessonContentRepository,
            IOptions<FileUploadSettings> settings, IFileStorageService fileService,IMapper mapper)
        {
            _repository = lessonContentRepository;
            _fileService = fileService;
            _mapper = mapper;
            _settings = settings.Value;
        }
        public async Task<Response<int>> Handle(CreateLessonContentCommand request, CancellationToken cancellationToken)
        {
            var lessonContent = _mapper.Map<LessonContent>(request);

            if (request.ContentType==LessonContentType.Video|| request.ContentType == LessonContentType.PDF)
            {
                if (request.File != null)
                {
                    long maxBytes = _settings.MaxFileSizeMB * 1024 * 1024;

                    string baseFolder = request.ContentType switch
                    {
                        LessonContentType.Video => _settings.VideoStoragePath,
                        LessonContentType.PDF => _settings.DocumentsStoragePath,
                        _ => _settings.DefaultStoragePath
                    };
                    using var stream = request.File.OpenReadStream();

                    // 3. Validate
                    if (!_fileService.IsValidFile(stream, request.File.FileName, request.File.ContentType, _settings.AllowedFileExtensions.ToArray(), _settings.AllowedFileMimeTypes.ToArray(), maxBytes))
                    {
                        return new Response<int>("File validation failed: Invalid type or size.");
                    }
                    stream.Position = 0;

                    var savedPath = await _fileService.SaveFileAsync(stream, request.File.FileName, baseFolder);
                    lessonContent.ContentUrl = savedPath;
                    lessonContent.FileName = request.File.FileName;
                    lessonContent.FileSizeBytes = request.File.Length;
                    lessonContent.MimeType = request.File.ContentType;

                    if (request.ContentType == LessonContentType.Video)
                        lessonContent.ProcessingStatus = VideoProcessingStatus.Pending;
                }
                else
                {
                    return new Response<int>("File is required for this content type.");
                }

            }
            else if (request.ContentType == LessonContentType.Text)
            {
                lessonContent.ContentUrl = null;
            }
            else if (request.ContentType == LessonContentType.ExternalLink)
            {
                lessonContent.ContentUrl = request.ExternalUrl;
            }

            if (lessonContent.DisplayOrder == 0)
            {
                lessonContent.DisplayOrder = await _repository.GetMaxOrderByLessonId(request.LessonId) + 1;
            }
            try
            {
                await _repository.AddAsync(lessonContent);
                return new Response<int>(lessonContent.Id);
            }
            catch (Exception)
            {
                // Cleanup: Delete the file if DB save fails 
                if (!string.IsNullOrEmpty(lessonContent.ContentUrl))
                {
                    _fileService.DeleteFile(lessonContent.ContentUrl);
                }
                throw new ApiException("Error occured while saving the file");
            }
        }
    }
}