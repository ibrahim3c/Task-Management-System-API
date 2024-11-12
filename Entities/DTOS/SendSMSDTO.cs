using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOS
{
    public class SendSMSDTO
    {
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string PhoneNumber { get; set; } = default!;
        [Required]
        public string Body { get; set; } = default!;

    }
}
