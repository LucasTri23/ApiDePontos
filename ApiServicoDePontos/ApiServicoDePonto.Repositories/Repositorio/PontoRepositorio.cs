using ApiServicoDePontos.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServicoDePonto.Repositories.Repositorio
{
    public class PontoRepositorio : Contexto
    {
        public PontoRepositorio(IConfiguration configuration) : base(configuration)
        {
        }

        public void Inserir(Ponto model)
        {
            string comandoSql = @"INSERT INTO Ponto 
                            (CpfFuncionario, @DataPonto, @JustificativaPonto) 
                                VALUES
                            (@CpfFuncionario, @DataPonto, @JustificativaPonto);";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CpfFuncionario", model.funcionario.CpfFuncionario);
                cmd.Parameters.AddWithValue("@DataPonto", model.DataPonto);
                cmd.Parameters.AddWithValue("@JustificativaPonto", model.JustificativaPonto);
                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(Ponto model)
        {
            string comandoSql = @"UPDATE Ponto 
                            (CpfFuncionario, @DataPonto, @JustificativaPonto) 
                                VALUES
                            (@CpfFuncionario, @DataPonto, @JustificativaPonto);";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CpfFuncionario", model.funcionario.CpfFuncionario);
                cmd.Parameters.AddWithValue("@DataPonto", model.DataPonto);
                cmd.Parameters.AddWithValue("@JustificativaPonto", model.JustificativaPonto);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o funcionário com cpf {model.funcionario.CpfFuncionario}");
            }
        }

        public bool SeExiste(string pontoFuncionario)
        {
            string comandoSql = @"SELECT COUNT(DataPonto) as total FROM Ponto WHERE DataPonto = @DataPonto";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@DataPonto", pontoFuncionario);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        public Ponto ObterPorCpf(string cpfFuncionario)
        {
            string comandoSql = @"SELECT * FROM Ponto WHERE CpfFuncionario = @CpfFuncionario";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CpfFuncionario", cpfFuncionario);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        var ponto = new Ponto();
                        ponto.funcionario.CpfFuncionario = Convert.ToString(rdr["CpfFuncionario"]);
                        return ponto;
                    }
                    else
                        return null;
                }
            }
        }

        public void DeletarPonto(Funcionario funcionario, DateTime ponto1)
        {
            if (funcionario.Cargo.ToString() == "Rh")
            {
                string comandoSql = @"DELETE FROM Ponto
                WHERE DataPonto = @DataPonto;";
                using (var cmd = new SqlCommand(comandoSql, _conn))
                {
                    cmd.Parameters.AddWithValue("@DataPonto", ponto1);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException($"Nenhum registro afetado para a data {ponto1}");
                }
            }
            else
            {
                throw new Exception("Somente o Rh pode deletar um ponto.");
            }
        }

    }
}

 
    
    



