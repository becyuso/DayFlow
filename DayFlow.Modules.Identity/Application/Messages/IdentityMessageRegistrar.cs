using DayFlow.BuildingBlocks.Application.Messages;

namespace DayFlow.Modules.Identity.Application.Messages
{
    public sealed class IdentityMessageRegistrar
        : IMessageRegistrar
    {
        public void Register(
            IMessageRegistry registry)
        {
            //registry.Add(
            //    NoteMessageCodes.NotebookNotFound,
            //    "找不到此筆記本");
        }
    }
}
