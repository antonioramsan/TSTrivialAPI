using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TSTrivialAPI.Domain;

namespace TSTrivialAPI
{
    public class TSResponse
    {
        public TSResponse()
        {
        }

        public List<Dictionary<string, object>> select(List<Model> instances)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (Model item in instances)
            {
                var dtoname = item.GetDTOName();
                if (dtoname != "DTO")
                {
                    dtoname = "DTO_" + dtoname;
                }
                bool nodto = false;
                MethodInfo piInstance3 = item.GetType().GetMethod(dtoname);
                Dictionary<string, object> properties = new Dictionary<string, object>();
                if (piInstance3 != null)
                {
                    properties = (Dictionary<string, object>)piInstance3.Invoke(item, null);
                }
                else
                {
                    properties = item.GetProperties();
                    nodto = true;
                }


                Dictionary<string, object> any = new Dictionary<string, object>();
                if (nodto == false)
                {
                    foreach (var prop in properties)
                    {
                        string nestedtype2 = prop.Value.GetType().ToString();
                        if (nestedtype2.Split(".")[0] == "System")
                        {
                            any[prop.Key] = prop.Value;
                        }
                        else
                        {
                            List<Model> auxx = new List<Model>();
                            auxx.Add((Model)prop.Value);
                            var subaux = this.select(auxx);
                            any[prop.Key] = subaux[0];
                        }
                    }
                }
                else
                {

                    foreach (var prop in properties)
                    {
                        var valor = item.GetType().GetProperty(prop.Key).GetValue(item);
                        string nestedtype = valor.GetType().ToString();
                        any[prop.Key] = valor;
                    }

                }


                list.Add(any);
            }

            return list;
        }

    }
}
