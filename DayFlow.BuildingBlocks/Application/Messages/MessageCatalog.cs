namespace DayFlow.BuildingBlocks.Application.Messages
{
    public sealed class MessageCatalog
        : IMessageCatalog
    {
        private readonly IReadOnlyDictionary<string, string> _messages;

        public MessageCatalog(
            IEnumerable<IMessageRegistrar> registrars)
        {
            var registry =
                new MessageRegistry();

            foreach (var registrar in registrars)
            {
                registrar.Register(registry);
            }

            _messages = registry.Build();
        }

        public string Get(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return string.Empty;

            return _messages.TryGetValue(
                    code,
                    out var message)
                ? message
                : string.Empty;
        }
    }
}