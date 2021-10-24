using System;

namespace Supply.Application.DTOs.VeiculoDTOs
{
    public class UpdateVeiculoDTO
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public Guid VeiculoModeloId { get; set; }
    }
}
