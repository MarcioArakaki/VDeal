using System.Collections.Generic;
using System.Collections.ObjectModel;
using VehicleDealer.Persistence.DatabaseModel;

namespace VehicleDealer.ApplicationModels
{

    public class VehicleFeatureModel
    {
        public int VehicleId { get; set; }
        public int FeatureId { get; set; }

        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }

    }





}