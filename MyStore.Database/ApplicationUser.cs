using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyStore.Database
{
    public class ApplicationUser : IdentityUser
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }
       
        [JsonPropertyName("refreshTokenExpiry")]
        public DateTime? RefreshTokenExpiry { get; set; }
    }
}
