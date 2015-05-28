using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public enum Operator
    {
        [Code("==")]
        [Description("=")]
        Equal,

        [Code("!=")]
        [Description("!=")]
        NotEqual,

        [Code(">")]
        [Description(">")]
        GreaterThan,

        [Code("<")]
        [Description("<")]
        LessThan
    }
}