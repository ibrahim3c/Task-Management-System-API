using System.ComponentModel.DataAnnotations;

namespace Core.DTOS
{
    public class UpdateUserDTO
    {
        public string UserId {  get; set; }
        public string UserName { get; set; }

        public string? Address { get; set; } = default!;

        [Phone]
        public string? PhoneNumber {  get; set; }
    }
}
