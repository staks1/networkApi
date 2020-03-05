using Microsoft.EntityFrameworkCore;
using NetworkApi.Models;

namespace NetworkApi.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<NationalNetwork> NationalNetworks { get; set; }

        public DbSet<Line> Lines { get; set; }

        //insert users 
        public DbSet<User> Users { get; set; }

    }
}
