using Be.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Be.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {

        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Settings> Settings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
    }
}
