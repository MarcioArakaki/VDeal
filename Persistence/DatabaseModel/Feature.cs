using System.ComponentModel.DataAnnotations;

namespace VehicleDealer.Persistence.DatabaseModel
{
    public class Feature
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

    }
}