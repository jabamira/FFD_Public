using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FastFoodDelivery
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ItemMenu> ItemsMenu { get; set; } = null!;

        private const string DatabasePath = "C:\\Users\\artem\\Desktop\\FFD.db";
        public static bool UseMySql = true;

        public ApplicationContext()
        {
          
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (UseMySql)
            {
                optionsBuilder.UseMySql("Строка подключения(убрал тк. публичный репозиторй)",
                                new MySqlServerVersion(new Version(5, 7, 42)));
            }
            else 
            {
                optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
            }
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
