using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSTrivialAPI.Domain
{
    [TSTable("tblEstadosVT")]
    public class Estado : Model
    {
        [TSKey("id")]
        [TSField("intEstado")]
        public int id { get; set; }
        [TSField("strNombre")]
        public string Nombre { get; set; }
        [TSField("intPais")]
        public Pais Pais { get; set; }

        public Estado() {

        }
        public Estado(TSRequest request):base(request)
        {

        }

        public override List<string> subinstances()
        {
            List<string> subinstances = new List<string>()
            {
               "Pais"
            };
            return subinstances;
        }

    }
}
