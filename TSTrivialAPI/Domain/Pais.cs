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
        public override Dictionary<string, object> DTO()
        {
            return new Dictionary<string, object>() {
                {"code",id },
                {"name",Nombre}            
            };
        }

    }
}
