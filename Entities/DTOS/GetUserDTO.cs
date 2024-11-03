using System.ComponentModel.DataAnnotations;

namespace Core.DTOS
{
    public class GetUserDTO
    {
        public string UserId {  get; set; }
        public string UserName { get; set; }

        public string? Address { get; set; } = default!;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        public string? PhoneNumber {  get; set; }
    }
}
