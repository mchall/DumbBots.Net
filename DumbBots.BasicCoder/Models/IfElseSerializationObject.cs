using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class IfElseSerializationObject
    {
        public Condition Condition { get; set; }
        public object[] IfStatements { get; set; }
        public object[] ElseStatements { get; set; }
    }
}