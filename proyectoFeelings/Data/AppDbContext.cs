using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using proyectoFeelings.Models;

namespace proyectoFeelings.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Record> Record { get; set; }

       // public DbSet<Invoice> Invoice { get; set; }
        public DbSet<StoreProduct> StoreProduct { get; set; }



    }
}
