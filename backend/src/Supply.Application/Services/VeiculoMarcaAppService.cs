using AutoMapper;
using FluentValidation.Results;
using Supply.Application.Core.Application;
using Supply.Application.DTOs.VeiculoMarcaDTOs;
using Supply.Application.Interfaces;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Domain.Commands.VeiculoMarcaCommands;
using Supply.Domain.Core.Mediator;
using System;
using System.Threading.Tasks;

namespace Supply.Application.Services
{
    public class VeiculoMarcaAppService : AppService<VeiculoMarcaDTO, AddVeiculoMarcaDTO, UpdateVeiculoMarcaDTO, VeiculoMarcaCache>, 
        IVeiculoMarcaAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public VeiculoMarcaAppService(IMapper mapper,
                                      IMediatorHandler mediator,
                                      IVeiculoMarcaCacheRepository veiculoMarcaCacheRepository) 
            : base(mapper, veiculoMarcaCacheRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public override async Task<ValidationResult> Add(AddVeiculoMarcaDTO addVeiculoMarcaDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<AddVeiculoMarcaCommand>(addVeiculoMarcaDTO));
        }

        public override async Task<ValidationResult> Update(UpdateVeiculoMarcaDTO updateVeiculoMarcaDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<UpdateVeiculoMarcaCommand>(updateVeiculoMarcaDTO));
        }

        public override async Task<ValidationResult> Remove(Guid id)
        {
            return await _mediator.SendCommand(_mapper.Map<RemoveVeiculoMarcaCommand>(id));
        }
    }
}
