using FactureAbonnement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FactureAbonnement.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Abonnement> Abonnements { get; set; }
    }
}
