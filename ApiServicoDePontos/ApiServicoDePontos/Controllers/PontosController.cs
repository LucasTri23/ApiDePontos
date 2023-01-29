using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiServicoDePontos.Controllers
{
    [ApiController]
    public class PontosController : ControllerBase
    {
        [HttpGet("ponto")]
        public IActionResult ListarPontos()
        {
            return StatusCode(200);
        }
    }
}
