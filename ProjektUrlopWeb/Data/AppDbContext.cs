using Microsoft.EntityFrameworkCore;
using ProjektUrlopWeb.Models.Entities;

namespace ProjektUrlopWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options): base(options) { }
        public DbSet<Pracownik> Pracownicy { get; set; } = null!;
        public DbSet<Urlop> Urlopy { get; set; } = null!;
        public DbSet<Log> Logi { get; set; } = null!;

    }
}
