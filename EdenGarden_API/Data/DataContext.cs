using EdenGarden_API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EdenGarden_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>().HaveConversion<DateOnlyConverter>().HaveColumnType("date");
        }

        public DbSet<Room> Room { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<BookedRoom> BookedRoom { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<RoomType> RoomType { get; set; }

        internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            public DateOnlyConverter() : base(
                      d => d.ToDateTime(TimeOnly.MinValue),
                      d => DateOnly.FromDateTime(d))
            { }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
            {
                property.SetColumnType("timestamp");
            }
        }

    }
}
