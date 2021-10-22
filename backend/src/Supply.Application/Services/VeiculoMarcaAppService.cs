using AutoMapper;
using FluentValidation.Results;
using Supply.Application.DTOs.VeiculoMarcaDTOs;
using Supply.Application.Interfaces;
using Supply.Caching.Interfaces;
using Supply.Domain.Commands.VeiculoMarcaCommands;
using Supply.Domain.Core.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Application.Services
{
    public class VeiculoMarcaAppService : IVeiculoMarcaAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IVeiculoMarcaCacheRepository _veiculoMarcaCacheRepository;

        public VeiculoMarcaAppService(IMapper mapper,
                                      IMediatorHandler mediator,
                                      IVeiculoMarcaCacheRepository veiculoMarcaCacheRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _veiculoMarcaCacheRepository = veiculoMarcaCacheRepository;
        }

        public IEnumerable<VeiculoMarcaDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<VeiculoMarcaDTO>>(_veiculoMarcaCacheRepository.GetAll());
        }

        public VeiculoMarcaDTO GetById(Guid id)
        {
            return _mapper.Map<VeiculoMarcaDTO>(_veiculoMarcaCacheRepository.GetById(id));
        }

        public async Task<ValidationResult> Add(AddVeiculoMarcaDTO addVeiculoMarcaDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<AddVeiculoMarcaCommand>(addVeiculoMarcaDTO));
        }

        public async Task<ValidationResult> Update(UpdateVeiculoMarcaDTO updateVeiculoMarcaDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<UpdateVeiculoMarcaCommand>(updateVeiculoMarcaDTO));
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            return await _mediator.SendCommand(_mapper.Map<RemoveVeiculoMarcaCommand>(id));
        }
    }
}
