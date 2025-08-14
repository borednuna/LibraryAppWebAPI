using FluentValidation;
using LibraryAppWebAPI.DTOs;

namespace LibraryAppWebAPI.Validators;

public class BookValidator : AbstractValidator<BookDto>
{
    public BookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(1, 200).WithMessage("Title must be between 1 and 200 characters");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Author is required")
            .Length(1, 100).WithMessage("Author must be between 1 and 100 characters");

        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required")
            .Length(10, 13).WithMessage("ISBN must be 10 or 13 characters")
            .Matches(@"^\d{10}(\d{3})?$").WithMessage("ISBN must be numeric, 10 or 13 digits");

        RuleFor(x => x.Genre)
            .IsInEnum().WithMessage("Genre is required and must be a valid enum value");
    }
}
