using Microsoft.EntityFrameworkCore;
using Vega.Models;

namespace Vega.Persistence
{
    public class VegaDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public VegaDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .OwnsOne<Contact>(c => c.Contact, x =>
                {
                    x.Property(cn => cn.Name)
                        .HasColumnName("ContactName");

                    x.Property(cp => cp.Phone)
                        .HasColumnName("ContactPhone");

                    x.Property(ce => ce.Email)
                        .HasColumnName("ContactEmail");
                });

            modelBuilder.Entity<VehicleFeature>()
                .HasKey(x => new
                {
                    x.VehicleId,
                    x.FeatureId
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
