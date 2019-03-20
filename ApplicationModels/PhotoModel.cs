using System.ComponentModel.DataAnnotations;

namespace VehicleDealer.ApplicationModels
{
    public class PhotoModel
    {     
         public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        
        public int VehicleId { get; set; }        

    }    
}