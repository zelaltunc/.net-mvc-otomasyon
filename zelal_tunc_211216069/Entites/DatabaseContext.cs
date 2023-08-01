using Microsoft.EntityFrameworkCore;
namespace zelal_tunc_211216069.Entites
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
