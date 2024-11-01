using Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey(nameof(AppUser))]
       public string UserId { get; set; }
       public AppUser AppUser { get; set; }

    }
}
