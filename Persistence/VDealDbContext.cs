using Microsoft.EntityFrameworkCore;
using VehicleDealer.Persistence.DatabaseModel;


namespace VehicleDealer.Persistence
{
    public class VDealDbContext: DbContext
    {
        public VDealDbContext(DbContextOptions<VDealDbContext> options): base (options)
        {}
        
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; } 
        public DbSet<Vehicle> Vehicles { get; set; }             
    }
}