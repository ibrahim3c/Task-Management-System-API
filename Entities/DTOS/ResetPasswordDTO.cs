namespace Core.DTOS
{
    public class ResetPasswordDto
    {
        public string UserId { get; set; }
        public string code { get; set; }
        public string NewPassword { get; set; }
    }

}
