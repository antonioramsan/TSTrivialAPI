using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSTrivialAPI.Domain
{
    [TSTable("tblPaisesVT")]
    public class Pais : Model
    {
        [TSKey("intPais")]
        [TSField("intPais")]
        public int id { get; set; }
        [TSField("strNombre")]
        public string Nombre { get; set; }
        private TSRequest _request;

        public Pais(TSRequest request):base(request) {
            this._request = request;
            
        }
        //public override List<Model> select() {
            
        //    List<Model> lista = new List<Model>();
        //    lista.Add(new Pais(this._request) {id=1,Nombre="Mexico" });
        //    return lista;
        //}

    }
}
