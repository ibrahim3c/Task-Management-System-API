using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Owned] // weak entity=> depend on user table
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime? RevokedOn {  get; set; }
        public bool IsActive => RevokedOn==null && !IsExpired;


        // if u want to add UserId => u need to add Primary key to this table

        //[ForeignKey(nameof(AppUser))]
        //public string UserId {  get; set; }
        //public AppUser AppUser { get; set; }
    }

  

}
