using Microsoft.AspNetCore.Mvc;
using Supply.Application.DTOs.VeiculoMarcaDTOs;
using Supply.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Api.Controllers
{
    [Route("api/veiculo-marca")]
    public class VeiculoMarcaController : ApiController
    {
        private readonly IVeiculoMarcaAppService _veiculoMarcaAppService;

        public VeiculoMarcaController(IVeiculoMarcaAppService veiculoMarcaAppService)
        {
            _veiculoMarcaAppService = veiculoMarcaAppService;
        }

        [HttpGet]
        public IEnumerable<VeiculoMarcaDTO> Get()
        {
            return _veiculoMarcaAppService.GetAll();
        }

        [HttpGet("{id:guid}")]
        public VeiculoMarcaDTO Get(Guid id)
        {
            return _veiculoMarcaAppService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddVeiculoMarcaDTO addVeiculoMarcaDTO)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _veiculoMarcaAppService.Add(addVeiculoMarcaDTO));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateVeiculoMarcaDTO updateVeiculoMarcaDTO)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _veiculoMarcaAppService.Update(updateVeiculoMarcaDTO));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _veiculoMarcaAppService.Remove(id));
        }
    }
}
