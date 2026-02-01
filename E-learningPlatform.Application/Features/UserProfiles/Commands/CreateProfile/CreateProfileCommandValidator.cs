using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.UserProfiles.Commands.CreateProfile
{
    public class CreateProfileCommandValidator:AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileCommandValidator()
        {

            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Bio)
                .MaximumLength(2000)
                .When(x => !string.IsNullOrWhiteSpace(x.Bio));

            RuleFor(x => x.ProfilePictureUrl)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.ProfilePictureUrl));
        }
    }
}
