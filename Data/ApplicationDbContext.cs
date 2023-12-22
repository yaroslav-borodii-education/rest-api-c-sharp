using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1985, 5, 10),
                    Address = "123 Main St",
                    PhoneNumber = "555-1234",
                    Email = "john.doe@example.com"
                },
                new Patient
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1990, 8, 15),
                    Address = "456 Oak Ave",
                    PhoneNumber = "555-5678",
                    Email = "jane.smith@example.com"
                },
                new Patient
                {
                    Id = 3,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    DateOfBirth = new DateTime(1982, 3, 25),
                    Address = "789 Pine St",
                    PhoneNumber = "555-9012",
                    Email = "alice.johnson@example.com"
                },
                new Patient
                {
                    Id = 4,
                    FirstName = "Bob",
                    LastName = "Miller",
                    DateOfBirth = new DateTime(1978, 11, 8),
                    Address = "101 Cedar Ave",
                    PhoneNumber = "555-3456",
                    Email = "bob.miller@example.com"
                },
                new Patient
                {
                    Id = 5,
                    FirstName = "Eva",
                    LastName = "Davis",
                    DateOfBirth = new DateTime(1995, 6, 17),
                    Address = "202 Elm St",
                    PhoneNumber = "555-7890",
                    Email = "eva.davis@example.com"
                }
            );
        }
    }
}
