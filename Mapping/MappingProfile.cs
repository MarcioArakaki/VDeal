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
                    var removedFeatures = v.Features.Where(f => !vm.Features.Contains(f.FeatureId)).ToList();
                    removedFeatures.ForEach((f) => {v.Features.Remove(f);});
                        
                    //Add new Features                    
                    var addedFeatures = vm.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature{FeatureId = id}).ToList();
                    addedFeatures.ForEach((f) => {v.Features.Add(f);});                    
                });                                                
        }
    }
}