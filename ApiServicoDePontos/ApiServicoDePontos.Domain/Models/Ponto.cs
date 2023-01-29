using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ApiServicoDePontos.Domain.Models
{
    public class Ponto
    {
        public BigInteger Id { get; set; }
        public DateTime DataPonto { get; set; }
        public string? JustificativaPonto { get; set; }
        public Funcionario funcionario { get; set; }
    }
}
