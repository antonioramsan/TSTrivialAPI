/**
 * Copyright(c) Antonio Ramírez Santander
 * Copyright(c) TrivialSoft 2020.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TSTrivialAPI.Domain;


namespace TSTrivialAPI
{
    public class TSFactory
    {
        private TSContext _context;
        private TSRequest _request;
        public TSFactory()
        {
        }
        public TSFactory(TSContext context, TSRequest request)
        {
            this._context = context;
            this._request = request;
        }
        /// <summary>
        /// Devuelve una instancia de un modelo
        /// </summary>
        /// <param name="modelname">Nombre del modelo</param>
        /// <param name="id">Identificador de la instancia</param>
        /// <param name="level">Nivel de intanciación</param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        public Model instance(string modelname,
                        int id,
                        int level = 1,
                        string workspace = "")
        {
            TSRequest tsr = new TSRequest(modelname + "/" + id);
            Object[] args = { tsr };
            Model TrivialIntance = (Model)Activator.CreateInstance(Type.GetType("TSTrivialAPI.Domain." + modelname), args);
            // -- si el nivel de subinstanciacion es cero solo debolvemos un modelo en blanco
            if (level == 0)
            {
                return TrivialIntance;
            }

            // -- se recupera la instancia y se identifican las subentidades
            var instance = TrivialIntance.select(id) [0];
            List<string> subentities = instance.subinstances();
            
            // -- se inyecta cada una de las subinstancias
            foreach (string propname in subentities)
            {
                PropertyInfo piInstance = instance.GetType().GetProperty(propname);
                var newtype = piInstance.PropertyType.FullName;
                piInstance.SetValue(instance, this.instance(newtype.Split(".")[2], instance.getInstanceId(), level - 1));
            }
            return instance;
        }
        /// <summary>
        /// devuelve una lista de instancias de un modelo en base a ciertos creiterios de seleccion
        /// </summary>
        /// <param name="modelname">Nombre del modelo</param>
        /// <param name="filter">Filtro básico</param>
        /// <param name="level">Nivel de instanciación</param>
        /// <param name="order">Ordenamienro en base a propiedades</param>
        /// <param name="selection"></param>
        /// <param name="page">Criterio de paginación</param>
        /// <param name="loop"></param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        public List<Model> instances(string modelname,
                    Dictionary<string, object> filter,
                    int level = 1,
                    string order = "",
                    string selection = "",
                    string page = "",
                    int loop = 0,
                    string workspace = "")
        {

            if (level == 0)
            {
                return new List<Model>();
            }

            Object[] args = { this._request };
            Model TrivialIntance = (Model)Activator.CreateInstance(Type.GetType("TSTrivialAPI.Domain." + modelname), args);

            List<Model> instances = TrivialIntance.select(0);

            foreach (Model mod in instances)
            {
                List<string> subentities = mod.subinstances();
                foreach (string propname in subentities)
                {
                    PropertyInfo piInstance = mod.GetType().GetProperty(propname);
                    Model obj = (Model)mod.GetType().GetProperty(propname).GetValue(mod);
                    int identificador_default = obj.getInstanceId();

                    var nombre_key = obj.getKeyName();
                    int identificador = (int)obj.GetType().GetProperty(nombre_key).GetValue(obj);

                    int identificador_instancia = 0;
                    if (identificador > 0)
                    {
                        identificador_instancia = identificador;
                    }
                    else
                    {
                        identificador_instancia = identificador_default;
                    }

                    var newtype = piInstance.PropertyType.FullName;

                    if ((level - 1) > 0) {
                        var modd = this.instance(newtype.Split(".")[2], identificador_instancia, level - 1);
                        piInstance.SetValue(mod,modd );
                    }
                    
                }
            }
            return instances;
        }
    }
}
