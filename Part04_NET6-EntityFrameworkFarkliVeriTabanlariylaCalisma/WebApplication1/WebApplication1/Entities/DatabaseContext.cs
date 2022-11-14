using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Entities
{
    public class DatabaseContext : DbContext
    {
        protected DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
    }
}
