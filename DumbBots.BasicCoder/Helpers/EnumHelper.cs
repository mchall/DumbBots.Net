using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumbBots.BasicCoder
{
    public static class EnumHelper
    {
        public static T GetAttribute<T>(this Enum enumeration)
            where T : Attribute
        {
            return enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();
        }

        public static string GetDescription(this Enum enumeration)
        {
            var descAttr = enumeration.GetAttribute<DescriptionAttribute>();
            if (descAttr != null)
                return descAttr.Description;
            return enumeration.ToString();
        }
    }
}