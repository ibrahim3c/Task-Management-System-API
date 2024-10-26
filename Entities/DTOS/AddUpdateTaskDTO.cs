using Entities.Models;
using static Core.Constants.GeneralConsts;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOS
{
    public class AddUpdateTaskDTO
    {
        public int ProjectTaskId { get; set; } = default!;
        [MaxLength(100)]
        public string Title { get; set; } = default!;
        [MaxLength(500)]
        public string Description { get; set; } = default!;

        public ProjectTaskStatus Status { get; set; } = default!; // To Do, In Progress, Completed, Blocked

        public int ProjectId { get; set; } = default!;

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public IEnumerable<TaskAttachment> Attachments { get; set; } = new List<TaskAttachment>();
    }
}
