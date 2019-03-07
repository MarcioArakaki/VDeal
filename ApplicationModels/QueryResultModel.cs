using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VehicleDealer.ApplicationModels
{
    public class QueryResultModel<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}