using ApiServicoDePonto.Repositories.Repositorio;
using ApiServicoDePontos.Domain.Models;
using FuncionarioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiServicoDePontos.Controllers
{
    [Authorize]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioRepositorio _service;
        public FuncionarioController(FuncionarioRepositorio service)
        {
            _service = service;
        }

        [HttpGet("Listar Funcionario Por CPF/{cpfFuncionario}")]
        public IActionResult ObterPorCPF([FromRoute] string cpfFuncionario)
        {
            return StatusCode(200, _service.ObterPorCPF(cpfFuncionario));
        }

        [HttpGet("Listar Funcionarios")]
        public IActionResult Listar([FromQuery] string? nome)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var login = identity.FindFirst("login").Value;

            return StatusCode(200, _service.ListarFuncionario(nome));
        }

        [Authorize(Roles = "1")]
        [HttpPost("Inserir Funcionario")]
        public IActionResult Inserir([FromBody] Funcionario model)
        {
            try
            {
                _service.Inserir(model);
                return StatusCode(201);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }


        [Authorize(Roles = "1")]
        [HttpPut("Atualizar Funcionario")]
        public IActionResult Atualizar([FromBody] Funcionario model)
        {
            try
            {
                _service.Atualizar(model);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [Authorize(Roles = "1")]
        [HttpDelete("Deleatar Funcionario/{cpfFuncionario}")]
        public IActionResult Deletar([FromRoute] string cpfFuncionario)
        {
            _service.Deletar(cpfFuncionario);
            return StatusCode(200);
        }
    }
}
