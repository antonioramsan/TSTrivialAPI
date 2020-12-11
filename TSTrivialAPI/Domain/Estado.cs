using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSTrivialAPI.Domain
{
    [TSTable("tblEstadosVT")]
    public class Estado : Model
    {

        [TSField("intEstado")]
        public int id { get; set; }
        [TSField("strNombre")]
        public string Nombre { get; set; }

        public Estado() {

        }
        public Estado(TSRequest request):base(request)
        {

        }

        public override List<Model> select() {
            List<Model> lista = new List<Model>();
            lista.Add(new Estado() {id=1,Nombre="Veracruz" });
            return lista;
        }

    }
}
