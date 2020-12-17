using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSTrivialAPI.Domain
{
    [TSTable("Monedas")]
    public class Moneda: Model
    {
        public int id { get; set; }
        public string name { get; set; }
        public Moneda()
        {
        }
        public Moneda(TSRequest request) : base(request)
        {
        }
    }
}
