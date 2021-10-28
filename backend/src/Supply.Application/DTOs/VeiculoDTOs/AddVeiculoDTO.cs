using Supply.Application.Core.Application;
using System;

namespace Supply.Application.DTOs.VeiculoDTOs
{
    public class AddVeiculoDTO : DTO
    {
        public string Placa { get; set; }
        public Guid VeiculoModeloId { get; set; }
    }
}
