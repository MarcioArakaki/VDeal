using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleDealer.Extensions;
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

        public async Task<IEnumerable<Vehicle>> GetAllVehicles(VehicleQuery queryObj)
        {
            var query = context.Vehicles
            .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
            .Include(v => v.Model)
                .ThenInclude(m => m.Make)
            .AsQueryable();

            if (queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);

            if (queryObj.ModelId.HasValue)
                query = query.Where(v => v.ModelId == queryObj.ModelId.Value);

            //mapping string to experssion
            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
            };
            //columnsMap.Add("make", v => v.Model.Make.Name); --Without c#6

            query = query.ApplyOrdering(queryObj, columnsMap);
            query = query.ApplyPaging(queryObj);

            return await query.ToListAsync(); 
        }

    
    }
}