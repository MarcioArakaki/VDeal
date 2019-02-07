using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VehicleDealer.ApplicationModels
{
    public class MakeModel: KeyValuePairModel
    {
        public MakeModel()
        {
            Models = new Collection<KeyValuePairModel>();

        }

        public ICollection<KeyValuePairModel> Models { get; set; }
    }
}