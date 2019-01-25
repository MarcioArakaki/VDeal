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
            CreateMap<Persistence.DatabaseModel.VehicleModel, VehicleModelModel>();
            CreateMap<FeatureModel,Feature>();
            CreateMap<Vehicle, ApplicationModels.VehicleModel>();
        }
    }
}