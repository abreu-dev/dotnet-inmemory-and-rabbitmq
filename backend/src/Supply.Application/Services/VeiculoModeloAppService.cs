using AutoMapper;
using FluentValidation.Results;
using Supply.Application.Core.Application;
using Supply.Application.DTOs.VeiculoModeloDTOs;
using Supply.Application.Interfaces;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Domain.Commands.VeiculoModeloCommands;
using Supply.Domain.Core.Mediator;
using System;
using System.Threading.Tasks;

namespace Supply.Application.Services
{
    public class VeiculoModeloAppService : AppService<VeiculoModeloDTO, AddVeiculoModeloDTO, UpdateVeiculoModeloDTO, VeiculoModeloCache>,
        IVeiculoModeloAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public VeiculoModeloAppService(IMapper mapper,
                                      IMediatorHandler mediator,
                                      IVeiculoModeloCacheRepository veiculoModeloCacheRepository)
            : base(mapper, veiculoModeloCacheRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public override async Task<ValidationResult> Add(AddVeiculoModeloDTO addVeiculoModeloDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<AddVeiculoModeloCommand>(addVeiculoModeloDTO));
        }

        public override async Task<ValidationResult> Update(UpdateVeiculoModeloDTO updateVeiculoModeloDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<UpdateVeiculoModeloCommand>(updateVeiculoModeloDTO));
        }

        public override async Task<ValidationResult> Remove(Guid id)
        {
            return await _mediator.SendCommand(_mapper.Map<RemoveVeiculoModeloCommand>(id));
        }
    }
}
