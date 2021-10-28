using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Application.Core.Application
{
    public interface IAppService<TDTO, TAddDTO, TUpdateDTO> 
        where TDTO : DTO 
        where TAddDTO : DTO
        where TUpdateDTO : DTO
    {
        IEnumerable<TDTO> GetAll();
        TDTO GetById(Guid id);

        Task<ValidationResult> Add(TAddDTO addDTO);
        Task<ValidationResult> Update(TUpdateDTO updateDTO);
        Task<ValidationResult> Remove(Guid id);
    }
}
