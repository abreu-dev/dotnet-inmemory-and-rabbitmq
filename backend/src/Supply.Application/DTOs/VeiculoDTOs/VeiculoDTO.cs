using Supply.Application.Core.Application;
using Supply.Application.DTOs.VeiculoModeloDTOs;
using System;

namespace Supply.Application.DTOs.VeiculoDTOs
{
    public class VeiculoDTO : DTO
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Placa { get; set; }
        public VeiculoModeloDTO VeiculoModelo { get; set; }
    }
}
