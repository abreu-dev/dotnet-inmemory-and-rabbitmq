using FluentValidation.Results;
using Supply.Application.DTOs.VeiculoDTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Application.Interfaces
{
    public interface IVeiculoAppService
    {
        IEnumerable<VeiculoDTO> GetAll();
        VeiculoDTO GetById(Guid id);

        Task<ValidationResult> Add(AddVeiculoDTO addVeiculoDTO);
        Task<ValidationResult> Update(UpdateVeiculoDTO updateVeiculoDTO);
        Task<ValidationResult> Remove(Guid id);
    }
}
