using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleDealer.Persistence;
using VehicleDealer.Persistence.DatabaseModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VehicleDealer.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly VDealDbContext context;


        public VehiclesController(IMapper mapper, VDealDbContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] ApplicationModels.VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<ApplicationModels.VehicleModel, Vehicle>(vehicleModel);
            vehicle.LastUpdate = DateTime.Now;

            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, ApplicationModels.VehicleModel>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]ApplicationModels.VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await context.Vehicles.Include(x => x.Features).SingleOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
                return NotFound();

            mapper.Map<ApplicationModels.VehicleModel, Vehicle>(vehicleModel, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, ApplicationModels.VehicleModel>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await context.Vehicles.SingleOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
                return NotFound();

            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await context.Vehicles.Include(x => x.Features).SingleOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
                return NotFound();

            var result = mapper.Map<Vehicle, ApplicationModels.VehicleModel>(vehicle);

            return Ok(result);
        }
    }
}