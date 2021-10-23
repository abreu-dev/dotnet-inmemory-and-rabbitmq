using AutoMapper;
using FluentValidation.Results;
using Supply.Application.DTOs.VeiculoModeloDTOs;
using Supply.Application.Interfaces;
using Supply.Caching.Interfaces;
using Supply.Domain.Commands.VeiculoModeloCommands;
using Supply.Domain.Core.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Application.Services
{
    public class VeiculoModeloAppService : IVeiculoModeloAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IVeiculoModeloCacheRepository _veiculoModeloCacheRepository;

        public VeiculoModeloAppService(IMapper mapper,
                                      IMediatorHandler mediator,
                                      IVeiculoModeloCacheRepository veiculoModeloCacheRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _veiculoModeloCacheRepository = veiculoModeloCacheRepository;
        }

        public IEnumerable<VeiculoModeloDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<VeiculoModeloDTO>>(_veiculoModeloCacheRepository.GetAll());
        }

        public VeiculoModeloDTO GetById(Guid id)
        {
            return _mapper.Map<VeiculoModeloDTO>(_veiculoModeloCacheRepository.GetById(id));
        }

        public async Task<ValidationResult> Add(AddVeiculoModeloDTO addVeiculoModeloDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<AddVeiculoModeloCommand>(addVeiculoModeloDTO));
        }

        public async Task<ValidationResult> Update(UpdateVeiculoModeloDTO updateVeiculoModeloDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<UpdateVeiculoModeloCommand>(updateVeiculoModeloDTO));
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            return await _mediator.SendCommand(_mapper.Map<RemoveVeiculoModeloCommand>(id));
        }
    }
}
