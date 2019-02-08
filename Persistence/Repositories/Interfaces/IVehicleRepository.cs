using System.Threading.Tasks;
using VehicleDealer.Persistence.DatabaseModel;

namespace VehicleDealer.Persistence.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
       Task<Vehicle> GetVehicle(int id);
       Task<Vehicle> GetById(int id);
       void Add(Vehicle vehicle);
       void Remove(Vehicle vehicle);
       void Update(Vehicle vehicle);
       

    }

    
    
}