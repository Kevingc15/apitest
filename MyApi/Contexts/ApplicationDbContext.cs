using Microsoft.EntityFrameworkCore;
using MyApi.Entities;

namespace MyApi.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<Player> Players { get; set; }
    }
}
