using Supply.Application.Core.Application;
using Supply.Application.DTOs.VeiculoModeloDTOs;

namespace Supply.Application.Interfaces
{
    public interface IVeiculoModeloAppService : IAppService<VeiculoModeloDTO, AddVeiculoModeloDTO, UpdateVeiculoModeloDTO> { }
}
