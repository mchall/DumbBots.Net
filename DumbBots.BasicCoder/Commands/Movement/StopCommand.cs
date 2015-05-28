using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class StopCommand : Command
    {
        public override string Text
        {
            get { return "Stop"; }
        }

        public override string Code
        {
            get { return String.Format("api.Stop();"); }
        }

        public override SupportedType ReturnType
        {
            get { return SupportedType.Nothing; }
        }

        public override SupportedType ParameterType
        {
            get { return SupportedType.Nothing; }
        }

        public override Category Category
        {
            get { return BasicCoder.Category.Movement; }
        }
    }
}