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

        public override Dictionary<string, object> DTO()
        {
            return new Dictionary<string, object>() {
                {"code",id },
                {"name",Nombre},
                 {"country",Pais}
            };
        }

        public override List<string> subinstances()
        {
            return new List<string>()
            {
               "Pais"
            };
             //subinstances;
        }

    }
}
