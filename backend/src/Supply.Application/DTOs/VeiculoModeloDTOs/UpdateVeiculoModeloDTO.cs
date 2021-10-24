using System;

namespace Supply.Application.DTOs.VeiculoModeloDTOs
{
    public class UpdateVeiculoModeloDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid VeiculoMarcaId { get; set; }
    }
}
