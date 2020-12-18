/**
 * Copyright(c) Antonio Ramírez Santander
 * Copyright(c) TrivialSoft 2020.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSTrivialAPI
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class TSFieldAttribute : System.Attribute
    {
        public string name { get; set; }
        public TSFieldAttribute(string name)
        {
            this.name = name;
        }
    }
}