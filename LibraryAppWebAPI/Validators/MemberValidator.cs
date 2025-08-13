using FluentValidation;
using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.Validators;

public class MemberValidator : AbstractValidator<Member>
{
    public MemberValidator()
    {
        RuleFor(member => member.UserName)
            .NotEmpty()
            .WithMessage("User name is required.")
            .Length(2, 100)
            .WithMessage("Name must be between 2 and 100 characters.")
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("Name can only contain letters and spaces");

        RuleFor(member => member.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email address")
            .MaximumLength(100)
            .WithMessage("Email cannot exceed 100 characters");
    }
}
