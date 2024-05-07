using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Carro
    {
        public int ID { get; set; }
        public string Cor { get; set; }
        public string Placa { get; set; }
        public int AnoFabricacao { get; set; }
        public int QtdPorta { get; set; }
    }
}