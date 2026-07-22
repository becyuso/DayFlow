using FluentValidation;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Create
{
    public sealed class CreateValidator
        : AbstractValidator<CreateCommand>
    {
        public CreateValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Color)
                .NotNull()
                .NotEmpty()
                .Matches("^#([A-Fa-f0-9]{6})$")
                .WithMessage("Color 必須是 Hex 顏色格式，例:#000000");
        }
    }
}
