using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Project
    {
        [Required]
        public int ProjectId { get; set; } = default!;
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = default!;
        [MaxLength(500)]
        [Required]
        public string Description { get; set; } = default!;

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public IEnumerable<ProjectTask> Tasks { get; set; }

       // user

    }
}
