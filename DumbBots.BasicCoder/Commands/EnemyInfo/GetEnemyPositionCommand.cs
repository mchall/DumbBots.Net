using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class GetEnemyPositionCommand : Command
    {
        public override string Text
        {
            get { return "Get Enemy Position"; }
        }

        public override string Code
        {
            get { return String.Format("api.GetEnemyPosition();"); }
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
            get { return BasicCoder.Category.EnemyInformation; }
        }
    }
}