using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Quizzes.Commands.CreateCommand
{
    public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
    {
        public CreateQuizCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

            RuleFor(p => p.TimeLimitMinutes)
                .GreaterThan(0).WithMessage("Time limit must be at least 1 minute.");

            RuleFor(p => p.PassingScore)
                .GreaterThanOrEqualTo(0).WithMessage("Passing score cannot be negative.");

            RuleFor(p => p.Questions)
                .NotEmpty().WithMessage("A quiz must have at least one question.");

            RuleForEach(p => p.Questions).ChildRules(question =>
            {
                question.RuleFor(q => q.QuestionText).NotEmpty();
                question.RuleFor(q => q.Points).GreaterThan(0);

                // Ensure every question has at least two options and one is correct
                question.RuleFor(q => q.Options)
                    .Must(o => o.Count >= 2).WithMessage("Each question needs at least 2 options.")
                    .Must(o => o.Any(opt => opt.IsCorrect)).WithMessage("Each question must have at least one correct answer.");
            });

        }
    }
}
