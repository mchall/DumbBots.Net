using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class MoveToPointCommand : Command
    {
        public override string Text
        {
            get { return String.Format("Move to Point {0}", ParameterText); }
        }

        public override string Code
        {
            get { return String.Format("api.MoveToPoint({0});", ParameterCode); }
        }

        public override SupportedType ReturnType
        {
            get { return SupportedType.YesNo; }
        }

        public override SupportedType ParameterType
        {
            get { return SupportedType.Position; }
        }

        public override Category Category
        {
            get { return BasicCoder.Category.Movement; }
        }
    }
}