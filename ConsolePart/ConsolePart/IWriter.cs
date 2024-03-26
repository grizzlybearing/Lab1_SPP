using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePart
{
    internal interface IWriter
    {
        void Write(string serialized, string path);
    }
}
