using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VehicleDealer.ApplicationModels
{
    public class VehicleModel
    {
        public VehicleModel()
        {
        }
        public int Id { get; set; }

        public string ContactName { get; set; }
        public string ContactPhone { get; set; }

        public int VehicleId { get; set; }
        

    }
}