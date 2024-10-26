using Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOS
{
    public class GetTaskDTO
    {
        public int ProjectTaskId { get; set; } = default!;
        [MaxLength(100)]
        public string Title { get; set; } = default!;
        [MaxLength(500)]
        public string Description { get; set; } = default!;

        public string Status { get; set; } = default!; // To Do, In Progress, Completed, Blocked

        [Display(Name ="Project Name")]
        public string ProjectName { get; set; } = default!;

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;


    }
}
