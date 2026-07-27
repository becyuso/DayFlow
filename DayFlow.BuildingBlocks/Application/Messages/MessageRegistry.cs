namespace DayFlow.BuildingBlocks.Application.Messages
{
    public sealed class MessageRegistry
        : IMessageRegistry
    {
        private readonly Dictionary<string, string> _messages = new();

        public void Add(
            string code,
            string message)
        {
            if (!_messages.TryAdd(code, message))
            {
                throw new InvalidOperationException(
                    $"Duplicate message code: {code}");
            }
        }

        public IReadOnlyDictionary<string, string> Build()
        {
            return _messages;
        }
    }
}