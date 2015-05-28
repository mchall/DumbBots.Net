using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class GetEnemySightedCommand : Command
    {
        public override string Text
        {
            get { return "Get Enemy Sighted"; }
        }

        public override string Code
        {
            get { return String.Format("api.GetEnemySighted();"); }
        }

        public override SupportedType ReturnType
        {
            get { return SupportedType.YesNo; }
        }

        public override SupportedType ParameterType
        {
            get { return SupportedType.Nothing; }
        }

        public override Category Category
        {
            get { return BasicCoder.Category.EnemyInformation; }
        }
    }
}