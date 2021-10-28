using Supply.Application.Core.Application;
using System;

namespace Supply.Application.DTOs.VeiculoModeloDTOs
{
    public class AddVeiculoModeloDTO : DTO
    {
        public string Nome { get; set; }
        public Guid VeiculoMarcaId { get; set; }
    }
}
