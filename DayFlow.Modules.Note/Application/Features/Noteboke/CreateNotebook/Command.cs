using MediatR;
using Microsoft.Data.SqlClient;

namespace DayFlow.Modules.Note.Application.Features.Noteboke.CreateNotebook
{
    public record Command(Guid? userId, string? name, string? color, int? sortOrder) : IRequest<Result>;
}
