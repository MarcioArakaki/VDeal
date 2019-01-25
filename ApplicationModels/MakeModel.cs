using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VehicleDealer.ApplicationModels
{
    public class MakeModel
    {
        public MakeModel()
        {
            Models = new Collection<VehicleModelModel>();

        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<VehicleModelModel> Models { get; set; }
    }
}