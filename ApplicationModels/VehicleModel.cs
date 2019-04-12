using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VehicleDealer.ApplicationModels
{
    public class VehicleModel
    {
        public VehicleModel()
        {
            Features = new Collection<KeyValuePairModel>();
        }
        public int Id { get; set; }


        public ContactModel Contact { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }
        public KeyValuePairModel Model { get; set; }
        public KeyValuePairModel Make { get; set; }
        public ICollection<KeyValuePairModel> Features { get; set; }
    }
}