using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSTrivialAPI.Domain
{
    [TSTable("Usuarios")]
    public class Usuario : Model
    {
        public int intUsuario { get; set; }
        public string strNombre { get; set; }
        public string strUsuario { get; set; }

        public Dictionary<string, object> DTO_help()
        {
            return new Dictionary<string, object>() {
                {"id",intUsuario },
                {"descripcion",strNombre}
            };
        }
        public Usuario()
        {
        }
        public Usuario(TSRequest request) : base(request)
        {
        }
    }
}