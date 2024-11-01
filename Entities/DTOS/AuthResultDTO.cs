namespace Core.DTOS
{
    public class AuthResultDTO
    {
        public List<string>? Messages { get; set; }
        public string? Token { get; set; }
        public bool Success { get; set; }
        public DateTime? ExpiresOn { get; set; }

    }
}
