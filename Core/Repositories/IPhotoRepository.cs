using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDealer.Core.DomainModel;

namespace VehicleDealer.Core.Repositories
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotosOfVehicleAsync(int vehicleId);
        void RemovePhoto(Photo photo);
        Task<Photo> GetPhoto(int id);
    }
}