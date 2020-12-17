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
        public Model()
        {
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

        public string getKeyName()
        {
            foreach (var ss in this._request.keys)
            {
                return ss.Key;
            }
            return "";
        }
        public string getDefaultKeyName()
        {
            foreach (var ss in this._request.properties)
            {
                return ss.Key;
            }
            return "";
        }
        public int getInstanceId()
        {
            return this.InstanceId;
        }
        public void setInstanceId(int id)
        {
            // -- si explicitamente se especifica el identificador de instancia
            if (this.getKeyName() != "")
            {
                PropertyInfo piInstancex = this.GetType().GetProperty(this.getKeyName());
                piInstancex.SetValue(this, id);
            }
            // -- una copia si no se especifico el identificador
            this.InstanceId = id;
        }

        virtual public List<Model> select(int id = 0)
        {

            List<Model> lista = new List<Model>();

            Object[] args = { this._request };


            if (this._request.model !="")
            {
                DataTable dt = this.fillDataTable(id);
                foreach (DataRow row in dt.Rows)
                {
                    // -- una instancia en blanco
                    Model TrivialIntance = (Model)Activator.CreateInstance(Type.GetType("TSTrivialAPI.Domain." + this._request.model), args);

                    foreach (var prop in this._request.properties)
                    {
                        PropertyInfo piInstancex = TrivialIntance.GetType().GetProperty(prop.Key);

                        bool essub = false;
                        var listsubs = TrivialIntance.subinstances();
                        foreach (var str in listsubs)
                        {
                            if (str == prop.Key)
                            {
                                essub = true;
                                break;
                            }
                        }

                        if (essub == false)
                        {
                            piInstancex.SetValue(TrivialIntance, row[prop.Value]);
                        }
                        else
                        {
                            PropertyInfo piInstance2 = TrivialIntance.GetType().GetProperty(prop.Key);
                            var newtype = piInstance2.PropertyType.FullName;
                            TSRequest tsr = new TSRequest(newtype.Split(".")[2] + "/" + row[prop.Value]);
                            Object[] argssub = { tsr };
                            Model SubTrivialIntance = (Model)Activator.CreateInstance(Type.GetType("TSTrivialAPI.Domain." + newtype.Split(".")[2]), argssub);
                            MethodInfo piInstance3 = SubTrivialIntance.GetType().GetMethod("setInstanceId");
                            Object[] argsm = { row[prop.Value] };
                            piInstance3.Invoke(SubTrivialIntance, argsm);
                            piInstance2.SetValue(TrivialIntance, SubTrivialIntance);

                        }
                    }

                    lista.Add(TrivialIntance);
                }



            }

            return lista;
        }

        public DataTable fillDataTable(int id = 0)
        {
            string query = "";

            if (id == 0)
            {
                query = this.sqlselect;
            }
            else
            {
                string fieldname = "";
                if (this.getKeyName() != "")
                {
                    fieldname = this._request.properties[this.getKeyName()];
                } else
                {
                    if (this.getDefaultKeyName() != "")
                    {
                        fieldname = this._request.properties[this.getDefaultKeyName()];
                    }
                    else {
                        fieldname = "UNKNOW_KEY_FIELD_IN_MODEL";
                    }
                }
                

                query = this.sqlselect + " where " + fieldname + "=" + id;
            }

            SqlConnection sqlConn = new SqlConnection("Server=localhost;Database=trivial;User Id=sa;Password=314159;");
            sqlConn.Open();
            DataTable dt;
            try
            {
                SqlCommand cmd = new SqlCommand(query, sqlConn);

                 dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                throw;
            }
            

            sqlConn.Close();
            return dt;
        }
        virtual public List<string> subinstances()
        {

            List<string> subinstances = new List<string>()
            {

            };

            return subinstances;
        }
    }
}
