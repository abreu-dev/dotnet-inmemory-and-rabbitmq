using Supply.Application.DTOs.VeiculoModeloDTOs;
using System;

namespace Supply.Application.DTOs.VeiculoDTOs
{
    public class VeiculoDTO
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public VeiculoModeloDTO VeiculoModelo { get; set; }
    }
}
