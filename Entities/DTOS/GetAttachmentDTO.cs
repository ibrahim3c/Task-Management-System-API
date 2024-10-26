using Core.Constants;
using Microsoft.AspNetCore.Http;
using MyShop.Web.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOS
{
    public class GetAttachmentDTO
    {
        public int AttachmentId { get; set; }
        public string FileName { get; set; } = default!;
        [Required]

        public IFormFile File { get; set; } = default!;
        public string TaskName { get; set; }
    }
}
