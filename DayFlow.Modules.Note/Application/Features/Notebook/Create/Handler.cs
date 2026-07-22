using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.BuildingBlocks.Infrastructure.Identity;
using DayFlow.BuildingBlocks.Infrastructure.Time;
using DayFlow.Modules.Notes.Domain.Notebooks;
using MediatR;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Create
{
    internal class Handler : IRequestHandler<CreateCommand, Result<CreateResult>>
    {
        private readonly INotebookRepository _notebookRepository;
        private readonly IIdGenerator _idGenerator;
        private readonly IClock _iClock;

        public Handler(INotebookRepository notebookRepository, IIdGenerator idGenerator, IClock iClock) =>
            (_notebookRepository, _idGenerator, _iClock) = (notebookRepository, idGenerator, iClock);

        public async Task<Result<CreateResult>> Handle(
            CreateCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == null || request.UserId == Guid.Empty)
                return Result<CreateResult>.Fail("Invalid user");

            var time = _iClock.TaiwanNow;

            var entity = Domain.Notebooks.Notebook.Create(
                                      _idGenerator.NewId(),
                                      request.UserId.Value,
                                      request.Name ?? string.Empty,
                                      request.Color,
                                      request.SortOrder ?? 0,
                                      time,
                                      request.UserId.Value,
                                      time,
                                      request.UserId.Value);

            _notebookRepository.Add(entity);

            return Result<CreateResult>.Ok(
                new CreateResult
                {
                    NotebookId = entity.NotebookId
                });
        }
    }
}
