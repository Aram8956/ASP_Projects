using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Profile> Profiles => Set<Profile>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Hotel> Hotels => Set<Hotel>();
        public DbSet<Booking> Bookings => Set<Booking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //1:1 relationship between User and Profile
            modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne(u => u.User)
            .HasForeignKey<Profile>(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            //1:N relationship between User and Booking
            modelBuilder.Entity<User>()
           .HasMany(u => u.Bookings)
           .WithOne(u => u.User)
           .HasForeignKey(u => u.UserId)
           .OnDelete(DeleteBehavior.Cascade);

            //1:N relationship between Booking and Room
            modelBuilder.Entity<Room>()
            .HasMany(r => r.Bookings)
            .WithOne(b => b.Room)
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

            //1:N relationship between Hotel and Room
            modelBuilder.Entity<Hotel>()
            .HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Cascade);
            //also have N:N relationship between User and Room through Booking(dzuma copiloty)
            
            
            modelBuilder.Entity<Room>().HasIndex(r => r.HotelId);
            modelBuilder.Entity<Booking>().HasIndex(b => new { b.RoomId, b.CheckInDate, b.CheckOutDate });
            modelBuilder.Entity<Profile>().HasIndex(p => p.UserId).IsUnique();
        }
    }
}
