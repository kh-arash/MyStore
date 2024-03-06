using System.Text.Json.Serialization;

namespace MyStore.Database.Models.Authentication
{
    public class AuthenticationResponse
    {
        [JsonPropertyName("isAuthenticated")]
        public bool IsAuthenticated { get; set; }
        [JsonPropertyName("user")]
        public ApplicationUser User { get; set; }
        [JsonPropertyName("role")]
        public List<string>? Roles { get; set; }
    }
}
