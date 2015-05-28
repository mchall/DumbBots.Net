using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class GetMedkitCountCommand : Command
    {
        public override string Text
        {
            get { return "Get Number of Medkits"; }
        }

        public override string Code
        {
            get { return String.Format("api.GetNumberofVisibleMedkits();"); }
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
            get { return BasicCoder.Category.WorldInformation; }
        }
    }
}