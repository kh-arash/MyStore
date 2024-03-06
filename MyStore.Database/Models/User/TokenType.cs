using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyStore.Database.Models.User
{
    public class TokenType
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = null!;
        [JsonPropertyName("expiryTokenDate")]
        public DateTime ExpiryTokenDate { get; set; }
    }
}
