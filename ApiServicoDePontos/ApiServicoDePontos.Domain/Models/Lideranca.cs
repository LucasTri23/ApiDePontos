using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServicoDePontos.Domain.Models
{
    public class Lideranca
    {
        public int LiderancaId { get; set; }
        public string DescricaoEquipe { get; set; }
        public ICollection<Funcionario> Funcionarios { get; set; }
    }
}
