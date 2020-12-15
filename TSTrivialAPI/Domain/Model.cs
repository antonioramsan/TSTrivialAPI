/**
 * Copyright(c) Antonio Ramírez Santander
 * Copyright(c) TrivialSoft 2020.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
        public void setInstanceId(int id) {
            this.InstanceId = id;
        }

        virtual public List<Model> select()
        {
          
            List<Model> lista = new List<Model>();

            Object[] args = { this._request };
            

            if (this._request.model == "Ciudad") {

                DataTable dt = this.fillDataTable("");

                foreach (DataRow row in dt.Rows) {
                    // -- una instancia en blanco
                    Model TrivialIntance = (Model)Activator.CreateInstance(Type.GetType("TSTrivialAPI.Domain." + this._request.model), args);

                    foreach (var prop in this._request.properties) {
                        PropertyInfo piInstancex = TrivialIntance.GetType().GetProperty( prop.Key);

                        bool essub = false;
                        var listsubs = TrivialIntance.subinstances();
                        foreach (var str in listsubs) {
                            if (str == prop.Key) {
                                essub = true;
                                break;
                            }  
                        }

                        if (essub == false)
                        {
                            piInstancex.SetValue(TrivialIntance, row[prop.Value]);
                        }
                        else {
                            PropertyInfo piInstance2 = TrivialIntance.GetType().GetProperty(prop.Key);
                            var newtype = piInstance2.PropertyType.FullName;
                            Model SubTrivialIntance = (Model)Activator.CreateInstance(Type.GetType("TSTrivialAPI.Domain." + newtype.Split(".")[2]), args);
                            MethodInfo piInstance3 = SubTrivialIntance.GetType().GetMethod("setInstanceId");
                            Object[] argsm = { row[prop.Value] };
                            piInstance3.Invoke(SubTrivialIntance, argsm);
                            piInstance2.SetValue(TrivialIntance, SubTrivialIntance);

                        }
                        

                    }


                        // -- establecer el valora para la propiedad id
                    //    PropertyInfo piInstance1 = TrivialIntance.GetType().GetProperty("id");
                    //piInstance1.SetValue(TrivialIntance, (int)row["intCiudad"] );
                    // -- establecer el valor para la propiedad Nombre
                    //PropertyInfo piInstance = TrivialIntance.GetType().GetProperty("Nombre");
                    //piInstance.SetValue(TrivialIntance, (string)row["strNombre"]);
                    // -- establecer el valor para la propiedad Estado( Una SubEntidad)
                    //PropertyInfo piInstance2 = TrivialIntance.GetType().GetProperty("Estado");
                    //var newtype = piInstance2.PropertyType.FullName;
                    //Model SubTrivialIntance = (Model)Activator.CreateInstance(Type.GetType("TSTrivialAPI.Domain." + newtype.Split(".")[2]), args);
                    //MethodInfo piInstance3 = SubTrivialIntance.GetType().GetMethod("setInstanceId");
                    //Object[] argsm = { (int)row["intEstado"] };
                    //piInstance3.Invoke(SubTrivialIntance, argsm);
                    //piInstance2.SetValue(TrivialIntance, SubTrivialIntance);

                    lista.Add(TrivialIntance);
                }

               
       
            }

            return lista;
        }

        public DataTable fillDataTable(string table)
        {
            string query = this.sqlselect;// "SELECT * FROM dstut.dbo." + table;

            SqlConnection sqlConn = new SqlConnection("Server=localhost;Database=trivial;User Id=sa;Password=314159;");
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            sqlConn.Close();
            return dt;
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
