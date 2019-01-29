using Microsoft.AspNetCore.Mvc;
using VehicleDealer.Persistence.DatabaseModel;

namespace VehicleDealer.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        [HttpPost]
        public IActionResult CreateVehicle([FromBody]Vehicle vehicle){

            return Ok(vehicle);
        }
    }
}