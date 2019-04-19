using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleDealer.Core.DomainModel;
using VehicleDealer.Core.Repositories;


namespace VehicleDealer.Persistence.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly VDealDbContext context;
        public PhotoRepository(VDealDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotosOfVehicleAsync(int vehicleId)
        {
            return await this.context.Photos.Where(p => p.VehicleId == vehicleId).ToListAsync();
        }

        public void RemovePhoto(Photo photo)
        {
            this.context.Photos.Remove(photo);
        }

        public Task<Photo> GetPhoto(int id)
        {
           return this.context.Photos.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}