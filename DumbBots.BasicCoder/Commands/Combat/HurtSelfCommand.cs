using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class HurtSelfCommand : Command
    {
        public override string Text
        {
            get { return String.Format("Hurt Self {0}", ParameterText); }
        }

        public override string Code
        {
            get { return String.Format("api.HurtSelf({0});", ParameterCode); }
        }

        public override SupportedType ReturnType
        {
            get { return SupportedType.Nothing; }
        }

        public override SupportedType ParameterType
        {
            get { return SupportedType.Number; }
        }

        public override Category Category
        {
            get { return BasicCoder.Category.Combat; }
        }
    }
}