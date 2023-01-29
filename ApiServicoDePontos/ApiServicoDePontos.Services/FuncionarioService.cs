using ApiServicoDePonto.Repositories.Repositorio;
using ApiServicoDePontos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncionarioService
{
    public class FuncionarioService
    {
        private readonly FuncionarioRepositorio _repositorio;
        public FuncionarioService(FuncionarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public List<Funcionario> Listar(string? nome)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarFuncionario(nome);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public Funcionario Obter(string cpfFuncionario)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.SeExiste(cpfFuncionario);
                return _repositorio.ObterPorCPF(cpfFuncionario);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Atualizar(Funcionario model)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(string cpfFuncionario)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Deletar(cpfFuncionario);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Funcionario model)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Inserir(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
    }
}

