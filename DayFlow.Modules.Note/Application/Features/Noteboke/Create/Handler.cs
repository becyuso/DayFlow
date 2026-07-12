using DayFlow.Modules.Note.Infrastructure.Database;
using MediatR;

using DomainEntities = DayFlow.Modules.Note.Domain.Entities;

namespace DayFlow.Modules.Note.Application.Features.Notebook.Create
{
    internal class Handler : IRequestHandler<CreateCommand, CreateResult>
    {
        private readonly NoteDbContext _db;

        public Handler(NoteDbContext db) => _db = db;

        public async Task<CreateResult> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null || request.UserId == System.Guid.Empty)
                return CreateResult.Fail("Invalid user");

            var id = System.Guid.NewGuid();

            var entity = new DomainEntities.Notebook(id,
                                      request.UserId.Value,
                                      request.Name ?? string.Empty,
                                      request.Color,
                                      request.SortOrder ?? 0,
                                      System.DateTime.UtcNow,
                                      request.UserId.Value);

            _db.Notebooks.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return CreateResult.Ok(id);
        }
    }
}
