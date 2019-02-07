using System.Threading.Tasks;
using VehicleDealer.Persistence.DatabaseModel;

namespace VehicleDealer.Persistence.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
       Task<Vehicle> GetVehicle(int id);
    }
    
}