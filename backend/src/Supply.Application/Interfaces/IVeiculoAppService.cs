using Supply.Application.Core.Application;
using Supply.Application.DTOs.VeiculoDTOs;

namespace Supply.Application.Interfaces
{
    public interface IVeiculoAppService : IAppService<VeiculoDTO, AddVeiculoDTO, UpdateVeiculoDTO> { }
}
