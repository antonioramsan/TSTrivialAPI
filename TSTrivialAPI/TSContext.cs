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
    public class TSContext
    {
        private TSRequest _request;
        public TSContext(string name, TSRequest request = null)
        {
            this._request = request;
        }
        public List<Model> read(string tquery, bool api = false)
        {
            TSRequest reqf = new TSRequest(tquery);

            Object[] args = { reqf };//{ this._request };
            Model TrivialIntance = (Model)Activator.CreateInstance(Type.GetType("TSTrivialAPI.Domain." + reqf.model), args);
            return TrivialIntance.select();
            //return new List<Model>();
        }
        List<Model> write(List<Model> datapack)
        {
            return new List<Model>();
        }

    }
}
