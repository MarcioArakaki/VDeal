using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleDealer.Persistence;
using VehicleDealer.Persistence.DatabaseModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VehicleDealer.Persistence.Repositories.Interfaces;
using VehicleDealer.Persistence.DataAbstraction.Interfaces;
using VehicleDealer.ApplicationModels;
using VehicleModel = VehicleDealer.ApplicationModels.VehicleModel;
using Microsoft.AspNetCore.Authorization;

namespace VehicleDealer.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly VDealDbContext context;

        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public VehiclesController(IMapper mapper, VDealDbContext context, IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateVehicle([FromBody] ApplicationModels.SaveVehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<ApplicationModels.SaveVehicleModel, Vehicle>(vehicleModel);
            vehicle.LastUpdate = DateTime.Now;

            repository.Add(vehicle);
            await unitOfWork.SaveAsync();


            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, ApplicationModels.VehicleModel>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]ApplicationModels.SaveVehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            mapper.Map<ApplicationModels.SaveVehicleModel, Vehicle>(vehicleModel, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            repository.Update(vehicle);

            await unitOfWork.SaveAsync();

            var result = mapper.Map<Vehicle, ApplicationModels.VehicleModel>(vehicle);

            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repository.GetById(id);

            if (vehicle == null)
                return NotFound();

            repository.Remove(vehicle);

            await unitOfWork.SaveAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            var result = mapper.Map<Vehicle, ApplicationModels.VehicleModel>(vehicle);

            return Ok(result);
        }

        [HttpGet]
        public async Task<QueryResultModel<VehicleModel>> GetVehicles(VehicleQueryModel filterModel)
        {
            var re = Request;
            var headers = re.Headers;
            var x = User.Claims;

            var filter = mapper.Map<VehicleQueryModel, VehicleQuery>(filterModel);
            var vehicles = await repository.GetAllVehicles(filter);

            var result = mapper.Map<QueryResult<Vehicle>, QueryResultModel<VehicleModel>>(vehicles);

            return result;
        }
    }
}