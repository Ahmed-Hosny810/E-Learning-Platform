using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandValidator:AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.TeacherId)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(5000);

            RuleFor(x => x.Language)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Length(3, 4);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Level)
                .IsInEnum();

            RuleFor(x => x.ThumbnailUrl)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.ThumbnailUrl));

            RuleFor(x => x.Requirements)
                .MaximumLength(4000)
                .When(x => !string.IsNullOrWhiteSpace(x.Requirements));

            RuleFor(x => x.WhatYouWillLearn)
                .MaximumLength(4000)
                .When(x => !string.IsNullOrWhiteSpace(x.WhatYouWillLearn));
        }
    }
}
