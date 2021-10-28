using Supply.Application.Core.Application;
using System;

namespace Supply.Application.DTOs.VeiculoModeloDTOs
{
    public class UpdateVeiculoModeloDTO : DTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid VeiculoMarcaId { get; set; }
    }
}
