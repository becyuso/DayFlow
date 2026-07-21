using System.Text.Json.Serialization;

namespace DayFlow.Web.UI.Components.Toast
{
    public sealed class ToastViewModel
    {
        [JsonPropertyName("type")]  
        public string? Type { get; init; }

        [JsonPropertyName("title")]
        public string? Title { get; init; }

        [JsonPropertyName("message")]
        public string? Message { get; init; }
    }
}
