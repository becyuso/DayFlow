using FluentValidation;

namespace DayFlow.Modules.Notes.Application.Features.Note.Create
{
    public sealed class CreateValidator
        : AbstractValidator<CreateCommand>
    {
        public CreateValidator()
        {
            RuleFor(x => x.UserId)
           .NotEmpty();

            RuleFor(x => x.NotebookId)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
