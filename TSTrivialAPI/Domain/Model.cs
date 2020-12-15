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
        private int InstanceId;
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

        public int getInstanceId() {
            return this.InstanceId;
        }

        virtual public List<Model> select()
        {
          
            List<Model> lista = new List<Model>();
            return lista;
        }
        virtual public  List<string> subinstances()
        {

            List<string> subinstances = new List<string>()
            {
               
            };

            return subinstances;
        }
    }
}
