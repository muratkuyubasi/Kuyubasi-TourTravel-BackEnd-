﻿using AutoMapper;
using TourV2.Data.Dto;
using System.Security.Claims;

namespace TourV2.Admin.Helpers.Mapping
{
    public class AppClaimProfile : Profile
    {
        public AppClaimProfile()
        {
            CreateMap<AppClaimDto, Claim>()
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ClaimType))
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ClaimValue))
               .ReverseMap();

          
        }
    }
}