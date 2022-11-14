using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Entities
{
    public class DatabaseContext : DbContext
    {
        protected DatabaseContext()
        {
#if DEBUG
#else
            Database.Migrate();
#endif
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
#if DEBUG
#else
            Database.Migrate();
#endif
        }

        public DbSet<Album> Albums { get; set; }
    }
}
