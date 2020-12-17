/**
 * Copyright(c) Antonio Ramírez Santander 
 * Copyright(c) TrivialSoft 2020.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace TSTrivialAPI
{
    public class TSRequest
    {
        public string table { get; set; }
        public Dictionary<string, string> properties { get; set; }
        public Dictionary<string, string> keys { get; set; }
        public string model { get; set; }
        /// <summary>
        /// Se refiere al nombre del modelo o de su nombre como una lista
        /// </summary>
        public string collectionname { get; set; }
        /// <summary>
        /// Se refiere al identificador de una instancia
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// se refiere al nivel de instanciacion de una instancia
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// se refiere a un criterio básico de filtrado de entidades
        /// </summary>
        public Dictionary<string, object> filter { get; set; }
        /// <summary>
        /// se refiere al dto de serializacion de una entidad
        /// </summary>
        public string dto { get; set; }
        /// <summary>
        /// se refiere al ordenamiento simple de las instancias recuperadas
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// se refiere a uno o varios metodos de la entidad
        /// </summary>
        public string actions { get; set; }
        /// <summary>
        /// se refiere a la paginacion de la respuesta
        /// </summary>
        public string page { get; set; }
        /// <summary>
        /// se refiere a la estructura de la respuesta 
        /// </summary>
        public string tql { get; set; }

        public TSRequest(string trivialquery) {
            string collectionname="";
            string param1="";
            string param2 = "";
            string param3 = "";
            string param4 = "";
            string param5 = "";
            string param6 = "";
            string param7 = "";

            var parameters = trivialquery.Split("/");
            collectionname = parameters[0];

            for (int i=1; i < parameters.Length ; i++) {
                switch (i) {
                    case 1:
                        param1 = parameters[i];
                        break;
                    case 2:
                        param2 = parameters[i];
                        break;
                    case 3:
                        param3 = parameters[i];
                        break;
                    case 4:
                        param4 = parameters[i];
                        break;
                    case 5:
                        param5 = parameters[i];
                        break;
                    case 6:
                        param6 = parameters[i];
                        break;
                    case 7:
                        param7 = parameters[i];
                        break;
                    default:
                        break;
                }
            }

            this.init(collectionname);

            this.SolveParameters(
             param1,
             param2,
             param3,
             param4,
             param5,
             param6,
             param7);

        }
        public TSRequest(
            string collectionname,
            string param1,
            string param2 = "",
            string param3 = "",
            string param4 = "",
            string param5 = "",
            string param6 = "",
            string param7 = "")
        {
            this.init(collectionname);

            this.SolveParameters(
             param1,
             param2,
             param3,
             param4,
             param5,
             param6,
             param7);
        }


        private void init(string collectionname) {
            this.table = "";
            this.actions = "";
            this.dto = "DTO";
            this.order = "";
            this.level = 1;
            this.page = "";
            this.tql = "";
            this.filter = new Dictionary<string, object>();
            this.collectionname = collectionname;
            this.model = this.GetModel(collectionname);

            this.properties = new Dictionary<string, string>();
            this.properties = this.GetFieldProperties(this.model);
            this.keys = this.GetKeyProperties(this.model);

            this.table = this.GetTableName(this.model);

            if (this.table == "")
            {
                this.table = this.model;
            }
        }


        /// <summary>
        /// Permite identificar el nombre del modelo
        /// </summary>
        /// <param name="collectionname"></param>
        /// <returns></returns>
        private string GetModel(string collectionname)
        {
            switch (collectionname)
            {
                case "paises":
                    return "Pais";
                    break;
                case "estados":
                    return "Estado";
                    break;
                case "ciudades":
                    return "Ciudad";
                    break;
                case "usuarios":
                    return "Usuario";
                    break;
                case "monedas":
                    return "Moneda";
                    break;
                default:
                    break;
            }
            return collectionname;
        }
        /// <summary>
        /// permite identificar cada elemento dependiendo de su funcion
        /// </summary>
        private void SolveParameters(
            string param1 = "",
            string param2 = "",
            string param3 = "",
            string param4 = "",
            string param5 = "",
            string param6 = "",
            string param7 = "")
        {
            SolveParameterValue(param1);
            SolveParameterValue(param2);
            SolveParameterValue(param3);
            SolveParameterValue(param4);
            SolveParameterValue(param5);
            SolveParameterValue(param6);
            SolveParameterValue(param7);
        }
        /// <summary>
        /// asigna el valor correspondiente
        /// </summary>
        /// <param name="param"></param>
        public void SolveParameterValue(string param)
        {
            string type = this.GetParameterType(param);
            switch (type)
            {
                case "ID":
                    this.id = Convert.ToInt32(param);
                    break;
                case "LEVEL":
                    this.level = Convert.ToInt32(param.Substring(1));
                    break;
                case "ORDER":
                    this.order = param;
                    break;
                case "DTO":
                    this.dto = param.Substring(1);
                    break;
                case "ACTION":
                    this.actions = param.Trim();
                    break;
                case "PAGE":
                    this.page = param;
                    break;
                case "FILTER":
                    var values = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(param);
                    this.filter = values;
                    break;
                case "TQL":
                    this.tql = param;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// resuelve el tipo de parametro
        /// </summary>
        /// <param name="param1"></param>
        /// <returns></returns>
        public string GetParameterType(string param1)
        {
            if (param1.Trim() == "")
                return "";
            if (this.IsNumeric(param1))
            {
                return "ID";
            }
            if (param1.Length == 2)
            {
                if (param1.Substring(0, 1) == "L" && this.IsNumeric(param1.Substring(1, 1)))
                {
                    return "LEVEL";
                }
            }
            if (param1.Substring(0, 1) == "_")
            {
                return "DTO";
            }
            if (param1.Contains("_desc") == true || param1.Contains("_asc") == true)
            {
                return "ORDER";
            }
            if (param1.Trim().Substring(0, 1) == "{" && param1.Trim().Substring(param1.Length - 1) == "}")
            {
                return "FILTER";
            }

            if (param1.Trim().Substring(0, 1) == "P" && param1.Contains("X") == true)
            {
                var arr = param1.Substring(1).Split("X");
                if (arr.Length == 2)
                {
                    if (this.IsNumeric(arr[0]) && this.IsNumeric(arr[1]))
                    {
                        return "PAGE";
                    }
                }
            }

            if (param1.Substring(0, 4).ToUpper() == "TQL:")
            {
                return "TQL";
            }

            return "ACTION";

        }

        public Dictionary<string, string> GetFieldProperties(string model)
        {
            Type type = Type.GetType("TSTrivialAPI.Domain." + model);
            PropertyInfo[] props = type.GetProperties();
            Dictionary<string, string> properties = new Dictionary<string, string>();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                bool find = false;
                foreach (object attr in attrs)
                {
                    string ss = attr.ToString();
                    if (ss == "TSTrivialAPI.TSFieldAttribute")
                    {
                        TSFieldAttribute TSAttr = attr as TSFieldAttribute;
                        properties[prop.Name] = TSAttr.name;
                        find = true;
                    }
                }
                if (find == false) {
                    properties[prop.Name] = prop.Name;
                }
            }


            //foreach (PropertyInfo prop in props)
            //{
            //    bool ss = false;
            //    foreach (var item in properties) {
            //        if (item.Key == prop) {
            //        }
            //    }
            //}

                return properties;
        }

        public string GetTableName(string model)
        {
            Type type = Type.GetType("TSTrivialAPI.Domain." + model);
            object[] attrs = type.GetCustomAttributes(true);

            foreach (object attr in attrs)
            {
                string ss = attr.ToString();
                if (ss == "TSTrivialAPI.TSTableAttribute")
                {
                    TSTableAttribute TSAttr = attr as TSTableAttribute;
                    return TSAttr.name;
                }
            }
            return model;
        }
        public Dictionary<string, string> GetKeyProperties(string model)
        {
            Type type = Type.GetType("TSTrivialAPI.Domain." + model);
            PropertyInfo[] props = type.GetProperties();
            Dictionary<string, string> properties = new Dictionary<string, string>();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);

                foreach (object attr in attrs)
                {
                    string ss = attr.ToString();
                    if (ss == "TSTrivialAPI.TSKeyAttribute")
                    {
                        TSKeyAttribute TSAttr = attr as TSKeyAttribute;
                        properties[prop.Name] = TSAttr.name;
                    }
                }
            }
            return properties;
        }

        /// <summary>
        /// determina si un string es numerico
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public Boolean IsNumeric(string valor)
        {
            int result;
            return int.TryParse(valor, out result);
        }

    }
}
