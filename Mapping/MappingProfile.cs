using System.Collections.Generic;
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
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vm => vm.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vm => vm.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vm => vm.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.MapFrom(vm => vm.Features.Select(id => new VehicleFeature {FeatureId = id})))
                .ForMember( v => v.LastUpdate, opt => opt.Ignore())
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vm,v) => {
                    //Remove unselected features
                    var removedFeatures = new List<VehicleFeature>();
                    foreach (var f in v.Features)
                        if(!vm.Features.Contains(f.FeatureId))
                            removedFeatures.Add(f);
                        foreach(var f in removedFeatures)
                            v.Features.Remove(f);
                    //Add new Features
                    foreach (var idFeature in vm.Features)
                        if(!v.Features.Any(f => f.FeatureId == idFeature))
                            v.Features.Add(new VehicleFeature{FeatureId = idFeature});
                } );
                                                
        }
    }
}