using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TSTrivialAPI.Domain
{
    [TSTable("tblCiudadesVT")]
    public class Ciudad : Model
    {
        [TSField("intCiudad")]
        public int id { get; set; }
        [TSField("strNombre")]
        public string Nombre { get; set; }
        [TSField("intEstado")]
        public Estado Estado { get; set; }      
        public Ciudad()
        {
        }
        public Ciudad(TSRequest request):base(request)
        {

        }

        public override Dictionary<string, object> DTO()
        {
            return new Dictionary<string, object>() {
                {"code",id },
                {"name",Nombre},
                {"state",Estado}
            };
        }
        public Dictionary<string, object> DTO_help()
        {
            return new Dictionary<string, object>() {
                {"id",id },
                {"descripcion",Nombre}
            };
        }

        public override List<string> subinstances() {

            List<string> subinstances = new List<string>()
            {
               "Estado"
            };

            return subinstances;
        }

        //public override List<Model> select()
        //{
        //    List<Model> lista = new List<Model>();
        //    lista.Add(new Ciudad() { id = 1, Nombre = "Tuxpan",Estado= new Estado() {id=1,Nombre="Veracruz" } });
        //    return lista;
        //}

    }
}
