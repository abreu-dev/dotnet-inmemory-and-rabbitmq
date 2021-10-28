using Supply.Application.Core.Application;
using Supply.Application.DTOs.VeiculoMarcaDTOs;
using System;

namespace Supply.Application.DTOs.VeiculoModeloDTOs
{
    public class VeiculoModeloDTO : DTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public VeiculoMarcaDTO VeiculoMarca { get; set; }
    }
}
