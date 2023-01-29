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

        public bool SeExiste(string funcionarioCpf)
        {
            string comandoSql = @"SELECT COUNT(CpfFuncionario) as total FROM Ponto WHERE CpfFuncionario = @CpfFuncionario";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CpfFuncionario", funcionarioCpf);
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

        public List<Ponto> ListarPonto(string cpf, DateTime ponto, string? justificativa)
        {
            string comandoSql = @"SELECT CpfFuncionario,DataPonto, JustificativaPonto WHERE CpfFuncionario = @CpfFuncionario";

            if (!string.IsNullOrWhiteSpace(cpf))
                comandoSql += " WHERE nome LIKE @nome";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                if (!string.IsNullOrWhiteSpace(cpf))
                    cmd.Parameters.AddWithValue("@nome", "%" + cpf + "%" + ponto + "%" + justificativa);

                using (var rdr = cmd.ExecuteReader())
                {
                    var x = new List<Ponto>();
                    while (rdr.Read())
                    {
                        var pontos = new Ponto();
                        pontos.funcionario.CpfFuncionario = Convert.ToString(rdr["CpfFuncionario"]);
                        pontos.DataPonto = Convert.ToDateTime(rdr["DataPonto"]);
                        pontos.JustificativaPonto = Convert.ToString(rdr["JustificativaPonto"]);

                    }
                    return x;
                }
            }
        }

        public void DeletarPonto(Funcionario funcionario, string ponto)
        {
            if (funcionario.Cargo.ToString() == "Rh")
            {
                string comandoSql = @"DELETE FROM Ponto
                WHERE DataPonto = @DataPonto;";
                using (var cmd = new SqlCommand(comandoSql, _conn))
                {
                    cmd.Parameters.AddWithValue("@DataPonto", ponto);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException($"Nenhum registro afetado para a data {ponto}");
                }
            }
            else
            {
                throw new Exception("Somente o Rh pode deletar um ponto.");
            }
        }

    }
}

 
    
    



