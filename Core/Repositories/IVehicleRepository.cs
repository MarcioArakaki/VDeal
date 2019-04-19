using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDealer.Core.DomainModel;

namespace VehicleDealer.Core.Repositories
{
    public interface IVehicleRepository
    {
       Task<Vehicle> GetVehicle(int id);
       Task<Vehicle> GetById(int id);
       Task<QueryResult<Vehicle>> GetAllVehicles(VehicleQuery filter);
       void Add(Vehicle vehicle);
       void Remove(Vehicle vehicle);
       void Update(Vehicle vehicle);
    }

    
    
}