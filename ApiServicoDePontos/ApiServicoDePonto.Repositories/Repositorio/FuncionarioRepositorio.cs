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
    public class FuncionarioRepositorio : Contexto
    {


        public FuncionarioRepositorio(IConfiguration configuration) : base(configuration)
        {
        }

        public void Inserir(Funcionario model)
        {
            string comandoSql = @"INSERT INTO Funcionario 
                                (CpfFuncionario, NomeFuncionario, NascimentoFuncionario, TelefoneFuncionario, EmailFuncionario, DataAdmissao, CargoId) 
                                    VALUES
                                (@CpfFuncionario, @NomeFuncionario, @NascimentoFuncionario, @TelefoneFuncionario, @EmailFuncionario, @DataAdmissao, @CargoId);";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CpfFuncionario", model.CpfFuncionario);
                cmd.Parameters.AddWithValue("@NomeFuncionario", model.NomeFuncionario);
                cmd.Parameters.AddWithValue("@NascimentoFuncionario", model.NascimentoFuncionario);
                cmd.Parameters.AddWithValue("@TelefoneFuncionario", model.TelefoneFuncionario);
                cmd.Parameters.AddWithValue("@EmailFuncionario", model.EmailFuncionario);
                cmd.Parameters.AddWithValue("@DataAdmissao", model.DataAdmissao);
                cmd.Parameters.AddWithValue("@CargoId", model.Cargo.CargoId);
                cmd.ExecuteNonQuery();
            }
        }
        public void Atualizar(Funcionario model)
        {
            string comandoSql = @"UPDATE Funcionario 
                            SET 
                                NomeFuncionario = @NomeFuncionario,
                                NascimentoFuncionario = @NascimentoFuncionario, 
                                TelefoneFuncionario = @TelefoneFuncionario,
                                EmailFuncionario = @EmailFuncionario,
                                DataAdmissao = @DataAdmissao,
                                CargoId = @CargoId,
                                LiderancaId = @LiderancaId,
                                FuncionarioEquipeId = @FuncionarioEquipeId
                            WHERE CpfFuncionario = @CpfFuncionario;";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CpfFuncionario", model.CpfFuncionario);
                cmd.Parameters.AddWithValue("@NomeFuncionario", model.NomeFuncionario);
                cmd.Parameters.AddWithValue("@NascimentoFuncionario", model.NascimentoFuncionario);
                cmd.Parameters.AddWithValue("@TelefoneFuncionario", model.TelefoneFuncionario);
                cmd.Parameters.AddWithValue("@EmailFuncionario", model.EmailFuncionario);
                cmd.Parameters.AddWithValue("@DataAdmissao", model.DataAdmissao);
                cmd.Parameters.AddWithValue("@CargoId", model.Cargo.CargoId);
                cmd.Parameters.AddWithValue("@LiderancaId", model.Lideranca.LiderancaId);
                cmd.Parameters.AddWithValue("@FuncionarioEquipeId", model.FuncionarioEquipe.FuncionarioEquipeId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o cpf {model.CpfFuncionario}");
            }
        }
        public bool SeExiste(string cpfFuncionario)
        {
            string comandoSql = @"SELECT COUNT(CpfFuncionario) as total FROM Funcionario WHERE CpfFuncionario = @CpfFuncionario";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CpfFuncionario", cpfFuncionario);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public Funcionario? ObterPorCPF(string cpfFuncionario)
        {
            string comandoSql = @"SELECT FuncionarioId, CpfFuncionario FROM Funcionario WHERE CpfFuncionario = @CpfFuncionario";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CpfCliente", cpfFuncionario);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        var funcionario = new Funcionario();
                        funcionario.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        funcionario.CpfFuncionario = Convert.ToString(rdr["CpfFuncionario"]);

                        return funcionario;
                    }
                    else
                        return null;
                }
            }
        }
        public List<Funcionario> ListarFuncionario(string? nome)
        {
            string comandoSql = @"SELECT CpfFuncionario, NomeFuncionario, NascimentoFuncionario, TelefoneFuncionario, EmailFuncionario, DataAdmissao, CargoId, LiderancaID, FuncionarioEquipeId FROM Cliente";

            if (!string.IsNullOrWhiteSpace(nome))
                comandoSql += " WHERE nome LIKE @nome";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                if (!string.IsNullOrWhiteSpace(nome))
                    cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var funcionario = new List<Funcionario>();
                    while (rdr.Read())
                    {
                        var funcionarios = new Funcionario();
                        funcionarios.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        funcionarios.CpfFuncionario = Convert.ToString(rdr["CpfFuncionario"]);
                        funcionarios.NomeFuncionario = Convert.ToString(rdr["NomeFuncionario"]);
                        funcionarios.NascimentoFuncionario = Convert.ToDateTime(rdr["NascimentoFuncionario"]);
                        funcionarios.TelefoneFuncionario = Convert.ToInt32(rdr["TelefoneFuncionario"]);
                        funcionarios.EmailFuncionario = Convert.ToString(rdr["EmailFuncionario"]);
                        funcionarios.DataAdmissao = Convert.ToDateTime(rdr["DataAdmissao"]);
                        funcionarios.Cargo.CargoId = Convert.ToInt32(rdr["CargoId"]);
                        funcionarios.Lideranca.LiderancaId = Convert.ToInt32(rdr["LiderancaId"]);
                        funcionarios.FuncionarioEquipe.FuncionarioEquipeId = Convert.ToInt32(rdr["FuncionarioEquipeId"]);
                    }
                    return funcionario;
                }
            }
        }
        public void Deletar(string cpfFuncionario)
        {
            string comandoSql = @"DELETE FROM Funcionario 
                                WHERE CpfFuncionario = @CpfFuncionario;";

            using (var cmd = new SqlCommand(comandoSql, _conn))
            {
                cmd.Parameters.AddWithValue("@CpfFuncionario", cpfFuncionario);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o cpf {cpfFuncionario}");
            }
        }
    }
}
