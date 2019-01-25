using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleDealer.Persistence.DatabaseModel
{
    public class Vehicle
    {
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

    }
}