using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class GetAmmoCommand : Command
    {
        public override string Text
        {
            get { return "Get Ammo"; }
        }

        public override string Code
        {
            get { return String.Format("api.GetAmmo();"); }
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
            get { return BasicCoder.Category.PlayerInformation; }
        }
    }
}