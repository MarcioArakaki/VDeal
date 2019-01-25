using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleDealer.Persistence.DatabaseModel
{
    [Table("VehicleModels")]
    public class VehicleModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public Make Make { get; set; }
        public int MakeId { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}