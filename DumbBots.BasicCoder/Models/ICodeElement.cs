using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumbBots.BasicCoder
{
    interface ICodeElement
    {
        string[] GetCode();
        object[] GetSerializationObjects();
        void Clear();
        void Populate(object[] objs);
    }
}