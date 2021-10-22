using AutoMapper;
using FluentValidation.Results;
using Supply.Application.DTOs.VeiculoDTOs;
using Supply.Application.Interfaces;
using Supply.Caching.Interfaces;
using Supply.Domain.Commands.VeiculoCommands;
using Supply.Domain.Core.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Application.Services
{
    public class VeiculoAppService : IVeiculoAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IVeiculoCacheRepository _veiculoCacheRepository;

        public VeiculoAppService(IMapper mapper,
                                 IMediatorHandler mediator,
                                 IVeiculoCacheRepository VeiculoCacheRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _veiculoCacheRepository = VeiculoCacheRepository;
        }

        public IEnumerable<VeiculoDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<VeiculoDTO>>(_veiculoCacheRepository.GetAll());
        }

        public VeiculoDTO GetById(Guid id)
        {
            return _mapper.Map<VeiculoDTO>(_veiculoCacheRepository.GetById(id));
        }

        public async Task<ValidationResult> Add(AddVeiculoDTO addVeiculoDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<AddVeiculoCommand>(addVeiculoDTO));
        }

        public async Task<ValidationResult> Update(UpdateVeiculoDTO updateVeiculoDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<UpdateVeiculoCommand>(updateVeiculoDTO));
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            return await _mediator.SendCommand(_mapper.Map<RemoveVeiculoCommand>(id));
        }
    }
}
