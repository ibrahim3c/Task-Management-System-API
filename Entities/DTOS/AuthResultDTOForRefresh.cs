using System.Text.Json.Serialization;

namespace Core.DTOS
{
    public class AuthResultDTOForRefresh
    {
        public List<string>? Messages { get; set; }
        public string? Token { get; set; }
        public bool Success { get; set; }

        // he will not sedn the refresh token in response but we will send it in cookie to frontend
        [JsonIgnore]
        public string? RefreshToken {  get; set; }
        public DateTime RefreshTokenExpiresOn { get; set; }


    }
}
