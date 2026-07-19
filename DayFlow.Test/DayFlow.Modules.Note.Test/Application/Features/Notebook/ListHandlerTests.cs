using DayFlow.BuildingBlocks.Application.Paging;
using DayFlow.Modules.Notes.Application.Features.Notebook.List;
using DayFlow.Modules.Notes.Infrastructure.Database;
using DayFlow.Test.Common.Fixtures;
using DayFlow.Test.DayFlow.Modules.Note.Test.TestData.Seeds;
using FluentAssertions;

namespace DayFlow.Modules.Note.Test.Application.Features.Notebook.List;

public class ListHandlerTests
    : IClassFixture<SqliteFixture<NoteDbContext>>
{
    private readonly NoteDbContext _db;

    public ListHandlerTests(
          SqliteFixture<NoteDbContext> fixture)
    {
         _db = fixture.Context;
    }

    [Fact]
    public async Task Handle_Should_Return_Notebooks()
    {
        // Arrange
        var userId = Guid.NewGuid();
        await NotebookSeed
            .SeedAsync(_db);

        var handler =
            new Handler(_db);

        var query =
            new Query(
                userId,
                null,
                new PagingRequest(
                    Page: 1,
                    PageSize: 20));

        // Act
        var result =
            await handler.Handle(
                query,
                CancellationToken.None);

        // Assert
        result.Success
            .Should()
            .BeTrue();

        result.Data
            .Should()
            .NotBeNull();

        result.Data!.Items
            .Should()
            .HaveCount(2);

        result.Data.Items[0]
            .Name
            .Should()
            .Be("工作筆記");
    }
}