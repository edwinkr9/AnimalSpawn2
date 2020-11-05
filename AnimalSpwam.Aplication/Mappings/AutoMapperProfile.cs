using AnimalSpawn.Domain.DTOs;
using AnimalSpawn.Domain.Entities;
using AutoMapper;
using System;

namespace AnimalSpwan.Aplication.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Animal, AnimalResponseDto>();
            CreateMap<Animal, AnimalRequestDto>();
            CreateMap<AnimalRequestDto, RfidTag>()
                .ForMember(destinatios => destinatios.Tag, act => act.MapFrom(source => source.RfidTag));
            CreateMap<AnimalRequestDto, Animal>()
                .ForMember(destination => destination.RfidTag, act => act.MapFrom(source => source))
                .AfterMap((src, dest)=> {
                dest.CreateAt = DateTime.Now;
                dest.CreatedBy = 3;
                dest.Status = true;
            });
            CreateMap<AnimalResponseDto, Animal>();
        }
    }
}
