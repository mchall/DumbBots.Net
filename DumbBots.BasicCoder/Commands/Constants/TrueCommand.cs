using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class TrueCommand : Command
    {
        public override string Text
        {
            get { return String.Format("Yes"); }
        }

        public override string Code
        {
            get { return String.Format("true"); }
        }

        public override SupportedType ReturnType
        {
            get { return SupportedType.YesNo; }
        }

        public override SupportedType ParameterType
        {
            get { return SupportedType.Nothing; }
        }

        public override Category Category
        {
            get { return BasicCoder.Category.Other; }
        }
    }
}