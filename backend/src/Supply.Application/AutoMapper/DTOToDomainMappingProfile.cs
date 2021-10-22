using AutoMapper;
using Supply.Application.DTOs.VehicleDTOs;
using Supply.Application.DTOs.VeiculoMarcaDTOs;
using Supply.Domain.Commands.VehicleCommands;
using Supply.Domain.Commands.VeiculoMarcaCommands;
using System;

namespace Supply.Application.AutoMapper
{
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateVehicleMap();
            CreateVeiculoMarcaMap();
        }

        private void CreateVehicleMap()
        {
            CreateMap<AddVehicleDTO, AddVehicleCommand>()
                .ConstructUsing(c => new AddVehicleCommand(c.Plate));

            CreateMap<UpdateVehicleDTO, UpdateVehicleCommand>()
                .ConstructUsing(c => new UpdateVehicleCommand(c.Id, c.Plate));

            CreateMap<Guid, RemoveVehicleCommand>()
                .ConstructUsing(c => new RemoveVehicleCommand(c));
        }

        private void CreateVeiculoMarcaMap()
        {
            CreateMap<AddVeiculoMarcaDTO, AddVeiculoMarcaCommand>()
                .ConstructUsing(c => new AddVeiculoMarcaCommand(c.Nome));

            CreateMap<UpdateVeiculoMarcaDTO, UpdateVeiculoMarcaCommand>()
                .ConstructUsing(c => new UpdateVeiculoMarcaCommand(c.Id, c.Nome));

            CreateMap<Guid, RemoveVeiculoMarcaCommand>()
                .ConstructUsing(c => new RemoveVeiculoMarcaCommand(c));
        }
    }
}
