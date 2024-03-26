using System.Xml.Linq;
using LibraryPart;

namespace ConsolePart.Serializers
{
    public class XML : ISerialize
    {
        public string Serialize(TraceResult traceResult)
        {
            XElement root = new XElement("root");

            foreach (var threadInfo in traceResult._threadList)
            {
                XElement thread = new XElement("thread",
                    new XAttribute("id", threadInfo.Key),
                    new XAttribute("time", threadInfo.Value._thread_time));

                foreach (var methodTrace in threadInfo.Value._methods)
                {
                    thread.Add(RecursiveMethodData(methodTrace));
                }

                root.Add(thread);
            }

            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);

            return doc.ToString();
        }

        private XElement RecursiveMethodData(MethodInfo methodTrace)
        {
            XElement result = new XElement("method",
                new XAttribute("name", methodTrace.name),
                new XAttribute("time", methodTrace.time),
                new XAttribute("class", methodTrace.classname));

            foreach (var nestedMethodTrace in methodTrace._nestedStack)
            {
                result.Add(RecursiveMethodData(nestedMethodTrace));
            }

            return result;
        }
    }
}
