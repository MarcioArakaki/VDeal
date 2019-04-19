using System;
using System.Threading.Tasks;
using VehicleDealer.Core;
using VehicleDealer.Core.DomainModel;

namespace VehicleDealer.Persistence.DataAbstraction
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private VDealDbContext context;
        private GenericRepository<Vehicle> vehicleRepository;

        public UnitOfWork(VDealDbContext context){
            this.context = context;
        }
        public GenericRepository<Vehicle> VehicleRepository
        {
            get
            {

                if (this.vehicleRepository == null)
                {

                    this.vehicleRepository = new GenericRepository<Vehicle>(context);
                }
                return vehicleRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


}