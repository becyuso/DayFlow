using DayFlow.BuildingBlocks.Identity;
using DayFlow.Modules.Note.Infrastructure.Database;
using MediatR;

using DomainEntities = DayFlow.Modules.Note.Domain.Entities;

namespace DayFlow.Modules.Note.Application.Features.Notebook.Create
{
    internal class Handler : IRequestHandler<CreateCommand, CreateResult>
    {
        private readonly NoteDbContext _db;
        private readonly IIdGenerator _idGenerator;

        public Handler(NoteDbContext db, IIdGenerator idGenerator) =>
            (_db, _idGenerator) = (db, idGenerator);

        public async Task<CreateResult> Handle(
            CreateCommand request, 
            CancellationToken cancellationToken)
        {
            if (request.UserId == null || request.UserId == System.Guid.Empty)
                return CreateResult.Fail("Invalid user");

            var entity = new DomainEntities.Notebook(
                                      _idGenerator.NewId(),
                                      request.UserId.Value,
                                      request.Name ?? string.Empty,
                                      request.Color,
                                      request.SortOrder ?? 0,
                                      System.DateTime.UtcNow,
                                      request.UserId.Value);

            _db.Notebooks.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return CreateResult.Ok(entity.NotebookId);
        }
    }
}
