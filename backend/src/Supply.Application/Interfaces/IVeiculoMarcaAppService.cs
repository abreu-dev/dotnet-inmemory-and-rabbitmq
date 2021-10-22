using FluentValidation.Results;
using Supply.Application.DTOs.VeiculoMarcaDTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Application.Interfaces
{
    public interface IVeiculoMarcaAppService
    {
        IEnumerable<VeiculoMarcaDTO> GetAll();
        VeiculoMarcaDTO GetById(Guid id);

        Task<ValidationResult> Add(AddVeiculoMarcaDTO addVeiculoMarcaDTO);
        Task<ValidationResult> Update(UpdateVeiculoMarcaDTO updateVeiculoMarcaDTO);
        Task<ValidationResult> Remove(Guid id);
    }
}
