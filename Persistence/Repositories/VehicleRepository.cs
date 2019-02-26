using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleDealer.Persistence.DatabaseModel;
using VehicleDealer.Persistence.Repositories.Interfaces;

namespace VehicleDealer.Persistence.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VDealDbContext context;
        public VehicleRepository(VDealDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> GetVehicle(int id)
        {
            return await context.Vehicles
                        .Include(v => v.Features)
                            .ThenInclude(vf => vf.Feature)
                        .Include(v => v.Model)
                            .ThenInclude(m => m.Make)
                                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Vehicle> GetById(int id)
        {
            return await context.Vehicles.SingleOrDefaultAsync(v => v.Id == id);
        }
        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }
        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
        public void Update(Vehicle vehicle)
        {

            context.Vehicles.Attach(vehicle);
            context.Entry(vehicle).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicles(Filter filter)
        {
            var query = context.Vehicles
            .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
            .Include(v => v.Model)
                .ThenInclude(m => m.Make)
            .AsQueryable();

            if (filter.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == filter.MakeId.Value);

            if (filter.ModelId.HasValue)
                query = query.Where(v => v.ModelId == filter.ModelId.Value);

            return await query.ToListAsync();

        }
    }
}