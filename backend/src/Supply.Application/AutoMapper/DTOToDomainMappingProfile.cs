using AutoMapper;
using Supply.Application.DTOs.VeiculoDTOs;
using Supply.Application.DTOs.VeiculoMarcaDTOs;
using Supply.Application.DTOs.VeiculoModeloDTOs;
using Supply.Domain.Commands.VeiculoCommands;
using Supply.Domain.Commands.VeiculoMarcaCommands;
using Supply.Domain.Commands.VeiculoModeloCommands;
using System;

namespace Supply.Application.AutoMapper
{
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateVeiculoMap();
            CreateVeiculoMarcaMap();
            CreateVeiculoModeloMap();
        }

        private void CreateVeiculoMap()
        {
            CreateMap<AddVeiculoDTO, AddVeiculoCommand>()
                .ConstructUsing(c => new AddVeiculoCommand(c.Placa));

            CreateMap<UpdateVeiculoDTO, UpdateVeiculoCommand>()
                .ConstructUsing(c => new UpdateVeiculoCommand(c.Id, c.Placa));

            CreateMap<Guid, RemoveVeiculoCommand>()
                .ConstructUsing(c => new RemoveVeiculoCommand(c));
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

        private void CreateVeiculoModeloMap()
        {
            CreateMap<AddVeiculoModeloDTO, AddVeiculoModeloCommand>()
                .ConstructUsing(c => new AddVeiculoModeloCommand(c.Nome, c.VeiculoMarcaId));

            CreateMap<UpdateVeiculoModeloDTO, UpdateVeiculoModeloCommand>()
                .ConstructUsing(c => new UpdateVeiculoModeloCommand(c.Id, c.Nome, c.VeiculoMarcaId));

            CreateMap<Guid, RemoveVeiculoModeloCommand>()
                .ConstructUsing(c => new RemoveVeiculoModeloCommand(c));
        }
    }
}
