namespace DayFlow.Modules.Notes.Application.Common.Exceptions
{
    public sealed class NoteDuplicateException
     : BusinessException
    {
        public NoteDuplicateException(
            string title)
            : base(
                $"{title} 已存在")
        {
        }

    }
}
