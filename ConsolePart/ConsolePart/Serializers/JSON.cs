using ConsolePart.Serializers;
using LibraryPart;
using Newtonsoft.Json;
using System;

namespace Tracer_Formating.Utils.Serialization
{
    public class JSON : ISerialize
    {
        public string Serialize(TraceResult traceResult)
        {
            // Сериализуем объект TraceResult в формат JSON
            return JsonConvert.SerializeObject(traceResult, Formatting.Indented);
        }
    }
}
