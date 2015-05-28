using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class SayTextCommand : Command
    {
        public override string Text
        {
            get { return String.Format("Say Text {0}", ParameterText); }
        }

        public override string Code
        {
            get { return String.Format("api.SayText({0});", ParameterCode); }
        }

        public override SupportedType ReturnType
        {
            get { return SupportedType.Nothing; }
        }

        public override SupportedType ParameterType
        {
            get { return SupportedType.Text; }
        }

        public override Category Category
        {
            get { return BasicCoder.Category.PlayerInformation; }
        }
    }
}