using System;

namespace Supply.Application.DTOs.VeiculoDTOs
{
    public class AddVeiculoDTO
    {
        public string Placa { get; set; }
        public Guid VeiculoModeloId { get; set; }
    }
}
