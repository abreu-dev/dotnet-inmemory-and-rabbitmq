using Microsoft.AspNetCore.Mvc;
using Supply.Application.DTOs.VeiculoModeloDTOs;
using Supply.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Api.Controllers
{
    [Route("api/veiculo-modelo")]
    public class VeiculoModeloController : ApiController
    {
        private readonly IVeiculoModeloAppService _veiculoModeloAppService;

        public VeiculoModeloController(IVeiculoModeloAppService veiculoModeloAppService)
        {
            _veiculoModeloAppService = veiculoModeloAppService;
        }

        [HttpGet]
        public IEnumerable<VeiculoModeloDTO> Get()
        {
            return _veiculoModeloAppService.GetAll();
        }

        [HttpGet("{id:guid}")]
        public VeiculoModeloDTO Get(Guid id)
        {
            return _veiculoModeloAppService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddVeiculoModeloDTO addVeiculoModeloDTO)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _veiculoModeloAppService.Add(addVeiculoModeloDTO));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateVeiculoModeloDTO updateVeiculoModeloDTO)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _veiculoModeloAppService.Update(updateVeiculoModeloDTO));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _veiculoModeloAppService.Remove(id));
        }
    }
}
