using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class MoveRandomCommand : Command
    {
        public override string Text
        {
            get { return "Move to Random Location"; }
        }

        public override string Code
        {
            get { return String.Format("api.MoveToRandomLocation();"); }
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