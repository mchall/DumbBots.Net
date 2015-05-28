using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class GetPositionCommand : Command
    {
        public override string Text
        {
            get { return "Get Position"; }
        }

        public override string Code
        {
            get { return String.Format("api.GetPosition();"); }
        }

        public override SupportedType ReturnType
        {
            get { return SupportedType.Position; }
        }

        public override SupportedType ParameterType
        {
            get { return SupportedType.Nothing; }
        }

        public override Category Category
        {
            get { return BasicCoder.Category.PlayerInformation; }
        }
    }
}