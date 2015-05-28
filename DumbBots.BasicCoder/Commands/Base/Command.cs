using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumbBots.BasicCoder
{
    public enum SupportedType
    {
        Nothing = 0,
        YesNo = 1,
        Number = 2,
        Position = 3,
        Text = 4,
    }

    public enum Category
    {
        Other = 0,
        Movement = 1,
        PlayerInformation = 2,
        WorldInformation = 3,
        EnemyInformation = 4,
        Combat = 5,
    }

    public abstract class Command
    {
        public abstract string Text { get; }
        public abstract string Code { get; }
        public abstract SupportedType ReturnType { get; }
        public abstract SupportedType ParameterType { get; }
        public abstract Category Category { get; }

        public Command ParameterCommand { get; set; }

        protected string ParameterCode
        {
            get
            {
                if (ParameterCommand == null)
                    return String.Empty;
                return ParameterCommand.Code.Replace(";", "");
            }
        }

        protected string ParameterText
        {
            get
            {
                if (ParameterCommand == null)
                    return String.Empty;
                return String.Format("({0})", ParameterCommand.Text);
            }
        }
    }
}