using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class AppUser:IdentityUser
    {
        [MaxLength(250)]
        public string Address { get; set; } = default!;
        public IEnumerable<Project> Projects { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
