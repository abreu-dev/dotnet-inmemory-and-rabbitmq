using AutoMapper;
using FluentValidation.Results;
using Supply.Caching.Core.Caching;
using Supply.Caching.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Application.Core.Application
{
    public abstract class AppService<TDTO, TAddDTO, TUpdateDTO, TCacheEntity> : IAppService<TDTO, TAddDTO, TUpdateDTO>
        where TDTO : DTO
        where TAddDTO : DTO
        where TUpdateDTO : DTO
        where TCacheEntity : CacheEntity
    {
        private readonly IMapper _mapper;
        private readonly ICacheRepository<TCacheEntity> _cacheRepository;

        protected AppService(IMapper mapper, ICacheRepository<TCacheEntity> cacheRepository)
        {
            _mapper = mapper;
            _cacheRepository = cacheRepository;
        }

        public IEnumerable<TDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<TDTO>>(_cacheRepository.GetAll());
        }

        public TDTO GetById(Guid id)
        {
            return _mapper.Map<TDTO>(_cacheRepository.GetById(id));
        }

        public abstract Task<ValidationResult> Add(TAddDTO addDTO);

        public abstract Task<ValidationResult> Update(TUpdateDTO updateDTO);

        public abstract Task<ValidationResult> Remove(Guid id);
    }
}
