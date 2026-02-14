using E_learningPlatform.Application.Settings;
using E_learningPlatform.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.LessonContents.Commands.CreateCommand
{
    public class CreateLessonContentCommandValidator : AbstractValidator<CreateLessonContentCommand>
    {
        public CreateLessonContentCommandValidator(IOptions<FileUploadSettings> settings)
        {
            var _settings = settings.Value;

            RuleFor(v => v.LessonId)
                .NotEmpty().WithMessage("LessonId is required.")
                .GreaterThan(0).WithMessage("Invalid LessonId.");

            // Validation for TEXT content
            When(v => v.ContentType == LessonContentType.Text, () => {
                RuleFor(v => v.TextContent)
                    .NotEmpty().WithMessage("Text content cannot be empty when ContentType is Text.");

                RuleFor(v => v.File)
                    .Null().WithMessage("You cannot upload a file for Text content.");
            });

            // Validation for FILE-based content (Video/PDF)
            When(v => v.ContentType == LessonContentType.Video || v.ContentType == LessonContentType.PDF, () => {
                RuleFor(v => v.File)
                    .NotNull().WithMessage("File is required for Video or PDF content.")
                    .Must(file => file.Length > 0).WithMessage("Uploaded file is empty.");

                // Check File Size (UX check before it hits the service)
                RuleFor(v => v.File)
                    .Must(file => file.Length <= (_settings.MaxFileSizeMB * 1024 * 1024))
                    .When(v => v.File != null)
                    .WithMessage($"File size exceeds the limit of {_settings.MaxFileSizeMB}MB.");

                // Check Extensions
                RuleFor(v => v.File)
                    .Must(file => {
                        var ext = Path.GetExtension(file.FileName).ToLower();
                        return _settings.AllowedFileExtensions.Contains(ext);
                    })
                    .When(v => v.File != null)
                    .WithMessage("Unsupported file extension.");
            });
        }
    }
}
