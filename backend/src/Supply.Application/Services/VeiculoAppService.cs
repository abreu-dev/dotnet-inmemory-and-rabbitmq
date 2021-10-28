using AutoMapper;
using FluentValidation.Results;
using Supply.Application.Core.Application;
using Supply.Application.DTOs.VeiculoDTOs;
using Supply.Application.Interfaces;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Domain.Commands.VeiculoCommands;
using Supply.Domain.Core.Mediator;
using System;
using System.Threading.Tasks;

namespace Supply.Application.Services
{
    public class VeiculoAppService : AppService<VeiculoDTO, AddVeiculoDTO, UpdateVeiculoDTO, VeiculoCache>,
        IVeiculoAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public VeiculoAppService(IMapper mapper,
                                 IMediatorHandler mediator,
                                 IVeiculoCacheRepository veiculoCacheRepository)
            : base(mapper, veiculoCacheRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public override async Task<ValidationResult> Add(AddVeiculoDTO addVeiculoDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<AddVeiculoCommand>(addVeiculoDTO));
        }

        public override async Task<ValidationResult> Update(UpdateVeiculoDTO updateVeiculoDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<UpdateVeiculoCommand>(updateVeiculoDTO));
        }

        public override async Task<ValidationResult> Remove(Guid id)
        {
            return await _mediator.SendCommand(_mapper.Map<RemoveVeiculoCommand>(id));
        }
    }
}
