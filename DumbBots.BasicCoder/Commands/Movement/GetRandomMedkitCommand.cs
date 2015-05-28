using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class GetRandomMedkitCommand : Command
    {
        public override string Text
        {
            get { return "Get Random Medkit"; }
        }

        public override string Code
        {
            get { return String.Format("api.GetRandomMedkit();"); }
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