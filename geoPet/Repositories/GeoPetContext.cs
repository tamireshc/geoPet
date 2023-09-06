using geoPet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace geoPet.Repositories
{
    public class GeoPetContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Ower> Owers { get; set; }
        public DbSet<Position> Positions { get; set; }

        public GeoPetContext(DbContextOptions<GeoPetContext> options)
      : base(options) { }
        public GeoPetContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("DOTNET_CONNECTION_STRING");

                optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=geoPet;User=SA;Password=Password12!;");
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>()
            .HasOne(o => o.Pet)
            .WithMany(x => x.Positions)
            .HasForeignKey(x => x.PositionId);

            modelBuilder.Entity<Pet>()
            .HasOne(o => o.Ower)
            .WithMany(x => x.Pets)
            .HasForeignKey(d => d.OwerId);

            modelBuilder.Entity<Pet>()
            .HasMany(o => o.Positions)
            .WithOne(x => x.Pet)
            .HasForeignKey(d => d.PetId);


            modelBuilder.Entity<Ower>()
            .HasMany(c => c.Pets)
            .WithOne(x => x.Ower)
            .HasForeignKey(d => d.OwerId);

             modelBuilder.Entity<Ower>()
            .HasIndex(e => e.Email)
            .IsUnique();
        }

    } 
}
