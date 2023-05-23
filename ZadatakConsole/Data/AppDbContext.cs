using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadatakConsole.Models;

namespace ZadatakConsole.Data
{
    public class AppDbContext : DbContext
    {
        private const string connectionString = @"Server=localhost;Database=ZadatakConsole;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
        public DbSet<User> Users { get; set; }
    }
}
