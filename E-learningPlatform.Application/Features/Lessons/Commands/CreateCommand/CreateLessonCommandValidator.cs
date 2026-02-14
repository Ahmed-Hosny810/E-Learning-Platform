using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Lessons.Commands.CreateCommand
{
    public class CreateLessonCommandValidator:AbstractValidator<CreateLessonCommand>
    {
        public CreateLessonCommandValidator()
        {
            RuleFor(p => p.SectionId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0);

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(200);

            RuleFor(p => p.DisplayOrder)
                .GreaterThanOrEqualTo(1);
        }
    }
}
