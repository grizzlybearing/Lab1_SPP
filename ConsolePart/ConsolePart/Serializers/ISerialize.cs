using LibraryPart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePart.Serializers
{
    internal interface ISerialize
    {
        string Serialize(TraceResult traceResult);
    }
}
