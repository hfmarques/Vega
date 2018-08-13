﻿using AutoMapper;
using System.Linq;
using Vega.Controllers.Resources;
using Vega.Models;
using Vega.Resources;

namespace Vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
            CreateMap<Contact, ContactResource>();
            CreateMap<Vehicle, VehicleResource>();

            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.Features,
                    x => x.MapFrom(vr => vr.Features.Select(id => new VehicleFeature
                    {
                        FeatureId = id
                    })));
        }
    }
}
