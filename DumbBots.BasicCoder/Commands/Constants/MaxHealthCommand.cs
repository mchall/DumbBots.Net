using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class MaxHealthCommand : Command
    {
        public override string Text
        {
            get { return String.Format("Max Health"); }
        }

        public override string Code
        {
            get { return String.Format("100"); }
        }

        public override SupportedType ReturnType
        {
            get { return SupportedType.Number; }
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