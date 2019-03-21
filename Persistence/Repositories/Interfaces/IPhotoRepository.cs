using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDealer.Persistence.DatabaseModel;

namespace VehicleDealer.Persistence.Repositories.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotosOfVehicleAsync(int vehicleId);
        void RemovePhoto(Photo photo);
        Task<Photo> GetPhoto(int id);
    }
}