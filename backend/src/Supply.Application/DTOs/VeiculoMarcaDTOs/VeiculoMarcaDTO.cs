using Supply.Application.Core.Application;
using System;

namespace Supply.Application.DTOs.VeiculoMarcaDTOs
{
    public class VeiculoMarcaDTO : DTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
