using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zelal_tunc_211216069.Entites
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(25)]
        public string FullName { get; set; }
        [Required]
        [StringLength(25)]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string Password{ get; set; }
        public bool Locked { get; set; }=false;
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        [Required]
        public string Role { get; set; } = "user";



    }
   
}
