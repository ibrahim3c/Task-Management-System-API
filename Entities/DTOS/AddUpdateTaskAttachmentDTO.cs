using System.ComponentModel.DataAnnotations;
using Core.Constants;
using Microsoft.AspNetCore.Http;
using MyShop.Web.Attributes;


namespace Core.DTOS
{
    public class AddUpdateTaskAttachmentDTO
    {
        public int AttachmentId { get; set; }

        [Required]
        [MaxLength(200)]
        public string FileName { get; set; } = default!;
        [Required]

        [AllowedExtenstion(FileSettings.AllowedExtensions),
        MaxFileSize(FileSettings.MaxFileSizeInMB)]
        [Display(Name = "Image")]
        public IFormFile File { get; set; } = default!;
        public string? FilePath { get; set; } = default!;

        public int ProjectTaskId { get; set; }

    }
}
