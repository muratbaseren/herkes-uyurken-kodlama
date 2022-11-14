using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    [Table("Albums")]
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }
    }
}
