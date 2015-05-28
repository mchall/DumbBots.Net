using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class CodeAttribute : Attribute
    {
        public string Code { get; private set; }

        public CodeAttribute(string code)
        {
            Code = code;
        }
    }
}