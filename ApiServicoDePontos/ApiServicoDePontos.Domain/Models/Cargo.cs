using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServicoDePontos.Domain.Models
{
    public class Cargo
    {
        public int CargoId { get; set; }
        public string DescricaoCargo { get; set; }
        public ICollection<Funcionario> Funcionarios { get; set; } // coleção de funcionários
    }
}
