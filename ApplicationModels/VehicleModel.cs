using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VehicleDealer.ApplicationModels
{
    public class VehicleModel
    {
        public VehicleModel()
        {
            Features = new Collection<FeatureModel>();
        }
        public int Id { get; set; }

        public ContactModel Contact { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }
        public VehicleModelModel Model { get; set; }
        public MakeModel Make { get; set; }
        public ICollection<FeatureModel> Features { get; set; }
    }
}