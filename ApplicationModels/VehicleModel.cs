using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using VehicleDealer.Persistence.DatabaseModel;

namespace VehicleDealer.ApplicationModels
{
    public class VehicleModel
    {
        public VehicleModel()
        {
            Features = new Collection<int>();
        }
        public int Id { get; set; }  

        public bool IsRegistered { get; set; }
        public ContactModel Contact { get; set; }              
        public VehicleModel Model { get; set; }
        public int ModelId { get; set; }

        public ICollection<int> Features { get; set; }
        

    }
}