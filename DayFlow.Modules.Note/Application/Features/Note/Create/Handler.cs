using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.BuildingBlocks.Infrastructure.Identity;
using DayFlow.BuildingBlocks.Infrastructure.Time;
using DayFlow.Modules.Notes.Domain.Notebooks;
using DayFlow.Modules.Notes.Domain.Notes;
using MediatR;

namespace DayFlow.Modules.Notes.Application.Features.Note.Create
{
    internal class Handler : IRequestHandler<CreateCommand, Result<CreateResult>>
    {
        private readonly INotebookRepository _notebookRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IIdGenerator _idGenerator;
        private readonly IClock _iClock;

        public Handler(INoteRepository noteRepository, INotebookRepository notebookRepository, IIdGenerator idGenerator, IClock iClock) =>
            (_noteRepository, _notebookRepository, _idGenerator, _iClock) = (noteRepository, notebookRepository, idGenerator, iClock);

        public async Task<Result<CreateResult>> Handle(
            CreateCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == default || request.UserId == Guid.Empty)
                return Result<CreateResult>.Fail("Invalid user");

            var notebook =
                await _notebookRepository.GetByIdAsync(request.NotebookId, cancellationToken);
            if (notebook == null)
                return Result<CreateResult>.Fail("Notebook not found");

            var time = _iClock.TaiwanNow;

            var entity = Domain.Notes.Note.Create(
                                      _idGenerator.NewId(),
                                      request.UserId,
                                      request.NotebookId,
                                      request.Title,
                                      request.Content ?? string.Empty,
                                      time,
                                      request.UserId,
                                      time,
                                      request.UserId);

            _noteRepository.Add(entity);

            return Result<CreateResult>.Ok(
                new CreateResult
                {
                    NoteId = entity.NoteId
                });
        }
    }
}
