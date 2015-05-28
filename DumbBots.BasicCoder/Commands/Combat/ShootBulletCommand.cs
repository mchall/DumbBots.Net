using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class ShootBulletCommand : Command
    {
        public override string Text
        {
            get { return String.Format("Shoot Bullet {0}", ParameterText); }
        }

        public override string Code
        {
            get { return String.Format("api.ShootBullet({0});", ParameterCode); }
        }

        public override SupportedType ReturnType
        {
            get { return SupportedType.Nothing; }
        }

        public override SupportedType ParameterType
        {
            get { return SupportedType.Position; }
        }

        public override Category Category
        {
            get { return BasicCoder.Category.Combat; }
        }
    }
}