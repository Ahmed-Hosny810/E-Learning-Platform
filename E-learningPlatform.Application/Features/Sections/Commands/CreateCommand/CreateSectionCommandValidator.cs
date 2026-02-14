using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Modules.Commands.CreateCommand
{
    public class CreateSectionCommandValidator:AbstractValidator<CreateSectionCommand>
    {
        public CreateSectionCommandValidator()
        {
            RuleFor(p => p.CourseId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be a valid ID.");

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.DisplayOrder)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be at least 1.");

            RuleFor(p => p.DurationMinutes)
                .GreaterThan(0).WithMessage("{PropertyName} must be a positive number.")
                .When(p => p.DurationMinutes.HasValue);
        }
    }
}
