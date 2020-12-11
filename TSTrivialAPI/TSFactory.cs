/**
 * Copyright(c) Antonio Ramírez Santander
 * Copyright(c) TrivialSoft 2020.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSTrivialAPI.Domain;

namespace TSTrivialAPI
{
    public class TSFactory
    {
        public TSFactory() {
        }
        public Model instance(string modelname,
                        int id,
                        int level = 1,
                        string workspace = "")
        {
            return new Model(); 
        }
        public List<Model> instances(string modelname,
                    Dictionary<string,object> filter,
                    int level = 1,
                    string order = "",
                    string selection = "",
                    string page= "",
                    int loop= 0,
                    string workspace = "") {
            return new List<Model>();
        }
    }
}
