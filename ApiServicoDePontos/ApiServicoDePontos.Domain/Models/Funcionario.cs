using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServicoDePontos.Domain.Models
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }
        public string NomeFuncionario { get; set; }
        public string CpfFuncionario { get; set; }
        public DateTime NascimentoFuncionario { get; set; }
        public DateTime DataAdmissao { get; set; }
        public int TelefoneFuncionario { get; set; }
        public string EmailFuncionario { get; set; }
        public ICollection<Ponto> Pontos { get; set; } // coleção de Pontos
        public Cargo Cargo { get; set; } 
        public Lideranca Lideranca { get; set; } 
        public FuncionarioEquipe FuncionarioEquipe { get; set; } 
    }
}
