using Microsoft.EntityFrameworkCore;
using ParkBee.Domain.Entities;

namespace ParkBee.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Garage> Garages { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<ParkingSession> Sessions { get; set; }
        public DbSet<Door> Doors { get; set; }
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

    }
}