using AutoMapper;
using VehicleDealer.ApplicationModels;
using VehicleDealer.Persistence.DatabaseModel;

namespace VehicleDealer.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Make,MakeModel>();
            CreateMap<VehicleModel,VehicleModelModel>();
            CreateMap<FeatureModel,Feature>();
        }
    }
}