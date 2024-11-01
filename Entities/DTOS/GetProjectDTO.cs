using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS
{
    public class GetProjectDTO
    {
        public int ProjectId { get; set; } = default!;

        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = default!;
        [MaxLength(500)]
        [Required]
        public string Description { get; set; } = default!;

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string UserId { get; set; } = default!;
    }
}
