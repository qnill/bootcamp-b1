using Microsoft.EntityFrameworkCore;
using net_core_bootcamp_b1.Models;

namespace net_core_bootcamp_b1.Database
{
    public class BootcampDbContext : DbContext
    {
        public BootcampDbContext(DbContextOptions<BootcampDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
    }
}