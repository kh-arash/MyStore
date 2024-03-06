using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyStore.Database.Models.User
{
    public class LoginResponse
    {
        [JsonPropertyName("accessToken")]
        public TokenType AccessToken { get; set; }
        [JsonPropertyName("refreshToken")]
        public TokenType RefreshToken { get; set; }
        [JsonPropertyName("user")]
        public ApplicationUser User { get; set; } = null!;
        [JsonPropertyName("roles")]
        public List<string>? Roles { get; set; }
    }
}
