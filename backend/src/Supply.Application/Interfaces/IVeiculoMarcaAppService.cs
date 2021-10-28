using Supply.Application.Core.Application;
using Supply.Application.DTOs.VeiculoMarcaDTOs;

namespace Supply.Application.Interfaces
{
    public interface IVeiculoMarcaAppService : IAppService<VeiculoMarcaDTO, AddVeiculoMarcaDTO, UpdateVeiculoMarcaDTO> { }
}
