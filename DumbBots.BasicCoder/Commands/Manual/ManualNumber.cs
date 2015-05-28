using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class ManualNumberCommand : Command
    {
        public int Value { get; set; }

        public override string Text
        {
            get { return String.Format("{0}", Value); }
        }

        public override string Code
        {
            get { return String.Format("{0}", Value); }
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