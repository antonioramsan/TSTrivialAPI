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
            TSRequest request = new TSRequest(tquery);
            TSFactory factory = new TSFactory(this, request);
            return factory.instances(request.model, request.filter, request.level, request.order, "", request.page, 1, "");
        }
        List<Model> write(List<Model> datapack)
        {
            return new List<Model>();
        }

    }
}
