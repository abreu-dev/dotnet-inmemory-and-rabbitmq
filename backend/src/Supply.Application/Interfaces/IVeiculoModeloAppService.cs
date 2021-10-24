using FluentValidation.Results;
using Supply.Application.DTOs.VeiculoModeloDTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Application.Interfaces
{
    public interface IVeiculoModeloAppService
    {
        IEnumerable<VeiculoModeloDTO> GetAll();
        VeiculoModeloDTO GetById(Guid id);

        Task<ValidationResult> Add(AddVeiculoModeloDTO addVeiculoModeloDTO);
        Task<ValidationResult> Update(UpdateVeiculoModeloDTO updateVeiculoModeloDTO);
        Task<ValidationResult> Remove(Guid id);
    }
}
