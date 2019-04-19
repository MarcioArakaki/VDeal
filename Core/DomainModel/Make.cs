using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleDealer.Core.DomainModel
{
    public class Make
    {
        public Make(){
            Models = new Collection<VehicleModel>();
        }
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<VehicleModel> Models { get; set; }
        
    }
}