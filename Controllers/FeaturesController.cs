using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleDealer.Persistence;
using Microsoft.EntityFrameworkCore;
using VehicleDealer.ApplicationModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using VehicleDealer.Core.DomainModel;

namespace VehicleDealer.Controllers
{
    [Route("api/[controller]")]
    public class FeaturesController : Controller
    {
        private readonly VDealDbContext context;
        private readonly IMapper mapper;
        public FeaturesController(VDealDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairModel>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();
            
            return mapper.Map<List<Feature>,List<KeyValuePairModel>>(features);
        }

    }
}