/**
 * Copyright(c) Antonio Ramírez Santander
 * Copyright(c) TrivialSoft 2020.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TSTrivialAPI;
using TSTrivialAPI.Domain;

namespace TSChapaAPI.Controllers
{
    [Route("api/trivial")]
    [ApiController]
    public class TrivialController : ControllerBase
    {
        [HttpGet("{collectionname}")]
        public ActionResult<object> Get(string collectionname)
        {
            TSContext context = new TSContext("chapa");
            return context.read(collectionname, true);
        }

        [HttpGet("{collectionname}/{param1}")]
        public ActionResult<object> Get(string collectionname, string param1)
        {
            TSContext context = new TSContext("chapa");
            var list = context.read(collectionname + "/" + param1, true);
            var response = new TSResponse();
            return response.select(list);
        }

        [HttpGet("{collectionname}/{param1}/{param2}")]
        public ActionResult<object> Get(string collectionname, string param1, string param2)
        {
            TSContext context = new TSContext("chapa");
            var list = context.read(collectionname + "/" + param1 + "/" + param2, true);
            var response = new TSResponse();
            return response.select(list);
        }

        [HttpGet("{collectionname}/{param1}/{param2}/{param3}")]
        public ActionResult<object> Get(string collectionname, string param1, string param2, string param3)
        {
            TSContext context = new TSContext("chapa");
            var list = context.read(collectionname + "/" + param1 + "/" + param2 + "/" + param3, true);
            var response = new TSResponse();
            return response.select(list);
        }

        [HttpGet("{collectionname}/{param1}/{param2}/{param3}/{param4}")]
        public ActionResult<object> Get(string collectionname, string param1, string param2, string param3, string param4)
        {
            TSContext context = new TSContext("chapa");
            var list = context.read(collectionname + "/" + param1 + "/" + param2 + "/" + param3 + "/" + param4, true);
            var response = new TSResponse();
            return response.select(list);
        }

        [HttpGet("{collectionname}/{param1}/{param2}/{param3}/{param4}/{param5}")]
        public ActionResult<object> Get(string collectionname, string param1, string param2, string param3, string param4, string param5)
        {
            TSContext context = new TSContext("chapa");
            var list = context.read(collectionname + "/" + param1 + "/" + param2 + "/" + param3 + "/" + param4 + "/" + param5, true);
            var response = new TSResponse();
            return response.select(list);
        }

        [HttpGet("{collectionname}/{param1}/{param2}/{param3}/{param4}/{param5}/{param6}")]
        public ActionResult<object> Get(string collectionname, string param1, string param2, string param3, string param4, string param5, string param6)
        {
            TSContext context = new TSContext("chapa");
            var list = context.read(collectionname + "/" + param1 + "/" + param2 + "/" + param3 + "/" + param4 + "/" + param5 + "/" + param6, true);
            var response = new TSResponse();
            return response.select(list);
        }

        [HttpGet("{collectionname}/{param1}/{param2}/{param3}/{param4}/{param5}/{param6}/{param7}")]
        public ActionResult<object> Get(string collectionname, string param1, string param2, string param3, string param4, string param5, string param6, string param7)
        {
            TSContext context = new TSContext("chapa");
            var list = context.read(collectionname + "/" + param1 + "/" + param2 + "/" + param3 + "/" + param4 + "/" + param5 + "/" + param6 + "/" + param7, true);
            var response = new TSResponse();
            return response.select(list);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
