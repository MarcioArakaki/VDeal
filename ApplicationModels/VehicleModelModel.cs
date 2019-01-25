using System.Collections.Generic;
using System.Collections.ObjectModel;
using VehicleDealer.Persistence.DatabaseModel;

namespace VehicleDealer.ApplicationModels
{
    public class VehicleModelModel
    {
        public VehicleModelModel()
        {
            this.Vehicles = new Collection<Vehicle>();
        }
        public int Id { get; set; }       
        public string Name { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }

    }
}