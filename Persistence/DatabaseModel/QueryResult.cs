using System.Collections.Generic;

namespace VehicleDealer.Persistence.DatabaseModel
{
    public class QueryResult<T>
    {    
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}