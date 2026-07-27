using DayFlow.BuildingBlocks.Application.Messages;

namespace DayFlow.Modules.Notes.Application.Messages
{
    public sealed class NoteMessageRegistrar
        : IMessageRegistrar
    {
        public void Register(
            IMessageRegistry registry)
        {
            registry.Add(
                NoteMessageCodes.NotebookNotFound,
                "找不到此筆記本");

            registry.Add(
                NoteMessageCodes.NotebookDeleted,
                "此筆記本已刪除");

            registry.Add(
                NoteMessageCodes.NotebookAccessDenied,
                "您沒有權限操作此筆記本");

            registry.Add(
                NoteMessageCodes.NoteNotFound,
                "找不到此筆記");

            registry.Add(
                NoteMessageCodes.NoteTitleTooLong,
                "標題不可超過200個字");
        }
    }
}
