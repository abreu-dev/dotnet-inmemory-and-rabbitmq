using Microsoft.AspNetCore.Mvc;

namespace Supply.Api.Controllers
{
    public class HealthCheckController : ApiController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
