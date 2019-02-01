using System;
using VehicleDealer.Persistence.DatabaseModel;

namespace VehicleDealer.DataAbstraction
{
    public class UnitOfWork : IDisposable
    {
        private VDealDbContext context;
        private GenericRepository<Vehicle> vehicleRepository;


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