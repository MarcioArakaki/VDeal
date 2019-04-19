using Microsoft.EntityFrameworkCore;
using VehicleDealer.Core.DomainModel;

namespace VehicleDealer
{
    public class VDealDbContext: DbContext
    {
        public VDealDbContext(DbContextOptions<VDealDbContext> options): base (options)
        {}
        
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; } 
        public DbSet<Vehicle> Vehicles { get; set; }      
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){

            modelBuilder.Entity<VehicleFeature>().HasKey(vf => 
                new { vf.VehicleId, vf.FeatureId});
        }       
    }
}