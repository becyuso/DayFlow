using MediatR;
using DayFlow.Modules.Note.Infrastructure.Database;
using DayFlow.Modules.Note.Domain.Entities;

namespace DayFlow.Modules.Note.Application.Features.Noteboke.CreateNotebook
{
    internal class Handler : IRequestHandler<CreateNotebook.Command, CreateNotebook.Result>
    {
        private readonly NoteDbContext _db;

        public Handler(NoteDbContext db) => _db = db;

        public async Task<CreateNotebook.Result> Handle(CreateNotebook.Command request, CancellationToken cancellationToken)
        {
            if (request.UserId == null || request.UserId == System.Guid.Empty)
                return CreateNotebook.Result.Fail("Invalid user");

            var id = System.Guid.NewGuid();

            var entity = new Notebook(id,
                                      request.UserId.Value,
                                      request.Name ?? string.Empty,
                                      request.Color,
                                      request.SortOrder ?? 0,
                                      System.DateTime.UtcNow,
                                      request.UserId.Value);

            _db.Notebooks.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return CreateNotebook.Result.Ok(id);
        }
    }
}
