using MultiContainerDockerApp.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace MultiContainerDockerApp.Dal
{
    public class MultiContainerDbContext : DbContext
    {

        public MultiContainerDbContext(DbContextOptions<MultiContainerDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<BloodType> BloodTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BloodType>().HasData(
                new BloodType() { Id = 1, Created = LocalDateTime.FromDateTime(DateTime.Now), Name = "AB+", Value = "AB", RhFactor = "+" },
                new BloodType() { Id = 2, Created = LocalDateTime.FromDateTime(DateTime.Now), Name = "AB-", Value = "AB", RhFactor = "-" },
                new BloodType() { Id = 3, Created = LocalDateTime.FromDateTime(DateTime.Now), Name = "A+", Value = "A", RhFactor = "+" },
                new BloodType() { Id = 4, Created = LocalDateTime.FromDateTime(DateTime.Now), Name = "A-", Value = "A", RhFactor = "-" },
                new BloodType() { Id = 5, Created = LocalDateTime.FromDateTime(DateTime.Now), Name = "B+", Value = "B", RhFactor = "+" },
                new BloodType() { Id = 6, Created = LocalDateTime.FromDateTime(DateTime.Now), Name = "B-", Value = "B", RhFactor = "-" },
                new BloodType() { Id = 7, Created = LocalDateTime.FromDateTime(DateTime.Now), Name = "O+", Value = "O", RhFactor = "+" },
                new BloodType() { Id = 8, Created = LocalDateTime.FromDateTime(DateTime.Now), Name = "O-", Value = "O", RhFactor = "-" }
                );
        }
    }
}
