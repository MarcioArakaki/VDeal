using System.Linq;
using AutoMapper;
using VehicleDealer.ApplicationModels;
using VehicleDealer.Persistence.DatabaseModel;

namespace VehicleDealer.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //Domain Api to Application Model
            CreateMap<Make,MakeModel>();
            CreateMap<Persistence.DatabaseModel.VehicleModel, VehicleModelModel>();
            CreateMap<Feature,FeatureModel>();
            CreateMap<VehicleDealer.Persistence.DatabaseModel.Vehicle,ApplicationModels.VehicleModel>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom( v => new ContactModel{ Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone}))
                .ForMember(vr => vr.Features, opt => opt.MapFrom( v=>  v.Features.Select(f => f.FeatureId)));


            //API Application Model to Domain
            CreateMap<ApplicationModels.VehicleModel, VehicleDealer.Persistence.DatabaseModel.Vehicle>()
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature {FeatureId = id})))
                .ForMember( v => v.LastUpdate, opt => opt.Ignore());
                                                
        }
    }
}