using ApiServicoDePonto.Repositories.Repositorio;
using ApiServicoDePontos.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace ApiServicoDePontos.Controllers
{
    [Authorize]
    [ApiController]
    public class PontosController : ControllerBase
    {
        private readonly PontoRepositorio _service;
        public PontosController(PontoRepositorio service)
        {
            _service = service;
        }

        [HttpGet("Listar Ponto por CPF/{cpfFuncionario}")]
        public IActionResult ObterPorCPF([FromRoute] string cpfFuncionario)
        {
            return StatusCode(200, _service.ObterPorCpf(cpfFuncionario));
        }

        [Authorize(Roles = "1")]
        [HttpPost("Inserir Ponto")]
        public IActionResult Inserir([FromBody] Ponto model)
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
        [HttpPut("AtualizarPonto")]
        public IActionResult Atualizar([FromBody] Ponto model)
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
        [HttpDelete("Deletar Ponto/{cpfFuncionario}")]
        public IActionResult Deletar([FromRoute] Funcionario funcionario, DateTime ponto)
        {
            _service.DeletarPonto(funcionario, ponto);
            return StatusCode(200);
        }

    }
}
