using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public class Condition
    {
        public Command LeftCommand { get; set; }
        public Operator Operator { get; set; }
        public Command RightCommand { get; set; }
    }
}