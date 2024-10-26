using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class TaskAttachment
    {
        
        public int AttachmentId { get; set; }

        [Required]
        [MaxLength(200)]
        public string FileName { get; set; } = default!;
        [Required]

        public string FilePath { get; set; } = default!;

        [ForeignKey(nameof(Task))]
        public int ProjectTaskId {  get; set; }

        public ProjectTask Task { get; set; } = default!;

    }
}
