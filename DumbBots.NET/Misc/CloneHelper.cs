using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Misc
{
    public static class CloneHelper
    {
        public static T Clone<T>(this T obj)
        {
            MemoryStream memory;
            BinaryFormatter formater;
            T clone;

            using (memory = new MemoryStream())
            {
                //Serialize ourselves
                formater = new BinaryFormatter();
                formater.Serialize(memory, obj);

                //Move the memory buffer to the start
                memory.Seek(0, SeekOrigin.Begin);

                //Undo the serialization in the new clone object
                clone = (T)formater.Deserialize(memory);

                return clone;
            }
        }
    }
}