using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VehicleDealer.Persistence;
using VehicleDealer.Persistence.DatabaseModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using VehicleDealer.ApplicationModels;

namespace VehicleDealer.Controllers
{
    [Route("api/[controller]")]
    public class MakesController : Controller
    {
        private readonly VDealDbContext context;
        private readonly IMapper mapper;
        public MakesController(VDealDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeModel>> GetMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();

            return mapper.Map<List<Make>,List<MakeModel>>(makes);
        }
    }
}