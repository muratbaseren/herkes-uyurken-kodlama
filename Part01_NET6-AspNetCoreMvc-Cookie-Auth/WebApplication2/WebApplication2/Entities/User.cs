using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string? FullName { get; set; } = null;

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public bool Locked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";
    }
}
