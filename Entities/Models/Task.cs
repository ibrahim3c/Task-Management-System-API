using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Constants.GeneralConsts;

namespace Entities.Models
{
    public class ProjectTask
    {
        public int ProjectTaskId { get; set; }=default!;
        [MaxLength(100)]
        public string Title { get; set; } = default!;
        [MaxLength(500)]
        public string Description { get; set; } = default!;

        public ProjectTaskStatus Status { get; set; } = default!; // To Do, In Progress, Completed, Blocked

        [ForeignKey(nameof(Project))]
        public int ProjectId {  get; set; } = default!;
        public Project Project { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public IEnumerable<TaskAttachment> Attachments { get; set; } = new List<TaskAttachment>();

    }

   
}
