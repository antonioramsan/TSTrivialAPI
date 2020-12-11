/**
 * Copyright(c) Antonio Ramírez Santander
 * Copyright(c) TrivialSoft 2020.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSTrivialAPI.Domain
{
    public class Model
    {
        private TSRequest _request;
        private string sqlselect = "";
        public Model() {
        }
         public Model(TSRequest request)
        {
            this._request = request;

            string sql = "";
            foreach (var prop in this._request.properties)
            {
                if (sql != "")
                {
                    sql += ",";
                }
                sql += prop.Value;
            }
            sql = "select " + sql + " from " + this._request.table;
            this.sqlselect = sql;
        }
        virtual public List<Model> select()
        {
          
            List<Model> lista = new List<Model>();
            return lista;
        }
    }
}
