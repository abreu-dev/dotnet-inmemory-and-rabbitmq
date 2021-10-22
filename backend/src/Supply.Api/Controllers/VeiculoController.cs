using Microsoft.AspNetCore.Mvc;
using Supply.Application.DTOs.VeiculoDTOs;
using Supply.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Api.Controllers
{
    public class VeiculoController : ApiController
    {
        private readonly IVeiculoAppService _veiculoAppService;

        public VeiculoController(IVeiculoAppService veiculoAppService)
        {
            _veiculoAppService = veiculoAppService;
        }

        [HttpGet]
        public IEnumerable<VeiculoDTO> Get()
        {
            return _veiculoAppService.GetAll();
        }

        [HttpGet("{id:guid}")]
        public VeiculoDTO Get(Guid id)
        {
            return _veiculoAppService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddVeiculoDTO addVeiculoDTO)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _veiculoAppService.Add(addVeiculoDTO));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateVeiculoDTO updateVeiculoDTO)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _veiculoAppService.Update(updateVeiculoDTO));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _veiculoAppService.Remove(id));
        }
    }
}
