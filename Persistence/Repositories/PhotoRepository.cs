using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleDealer.Persistence.DatabaseModel;
using VehicleDealer.Persistence.Repositories.Interfaces;

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
    }
}