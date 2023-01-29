using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServicoDePontos.Domain.Models
{
    public class FuncionarioEquipe
    {
        public int FuncionarioEquipeId { get; set; }
        public ICollection<Funcionario> Funcionarios{ get; set; } // coleção de Funcionario
        public ICollection<Funcionario> Funcionario { get; set; } // coleção de Lideranca
    }
}
