using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.BuildingBlocks.Infrastructure.Identity;
using DayFlow.BuildingBlocks.Infrastructure.Time;
using DayFlow.Modules.Notes.Infrastructure.Database;
using MediatR;

using DomainEntities = DayFlow.Modules.Notes.Domain.Entities;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Create
{
    internal class Handler : IRequestHandler<CreateCommand, Result<CreateResult>>
    {
        private readonly NoteDbContext _db;
        private readonly IIdGenerator _idGenerator;
        private readonly IClock _iClock;

        public Handler(NoteDbContext db, IIdGenerator idGenerator, IClock iClock) =>
            (_db, _idGenerator, _iClock) = (db, idGenerator, iClock);

        public async Task<Result<CreateResult>> Handle(
            CreateCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == null || request.UserId == System.Guid.Empty)
                return Result<CreateResult>.Fail("Invalid user");

            var time = _iClock.TaiwanNow;

            var entity = new DomainEntities.Notebook(
                                      _idGenerator.NewId(),
                                      request.UserId.Value,
                                      request.Name ?? string.Empty,
                                      request.Color,
                                      request.SortOrder ?? 0,
                                      time,
                                      request.UserId.Value,
                                      time,
                                      request.UserId.Value);

            _db.Notebooks.Add(entity);

            return Result<CreateResult>.Ok(
                new CreateResult
                {
                    NotebookId = entity.NotebookId
                });
        }
    }
}
