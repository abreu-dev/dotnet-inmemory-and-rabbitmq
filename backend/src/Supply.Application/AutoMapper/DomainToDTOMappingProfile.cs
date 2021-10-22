using AutoMapper;
using Supply.Application.DTOs.VehicleDTOs;
using Supply.Application.DTOs.VeiculoMarcaDTOs;
using Supply.Caching.Entities;

namespace Supply.Application.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<VehicleCache, VehicleDTO>();
            CreateMap<VeiculoMarcaCache, VeiculoMarcaDTO>();
        }
    }
}
