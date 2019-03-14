using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleDealer.Persistence.DatabaseModel
{
    public class Vehicle
    {
        public Vehicle(){
            Features = new Collection<VehicleFeature>();
            Photos = new Collection<Photo>();
        }
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }

        [StringLength(255)]
        public string ContactEmail { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }
        public VehicleModel Model { get; set; }
        public int ModelId { get; set; }
        public ICollection<VehicleFeature> Features { get; set; }
        public ICollection<Photo> Photos { get; set; }        
    }
}