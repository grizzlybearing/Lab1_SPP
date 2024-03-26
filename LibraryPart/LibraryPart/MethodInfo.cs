using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPart
{
    public class MethodInfo
    {
        public List<MethodInfo> _nestedStack = new List<MethodInfo>();
        private Stopwatch _stopwatch = new Stopwatch();

        public string name { get; }
        public string classname { get; }
        public long time { get; set; }

        internal MethodInfo(MethodBase mth)
        {
            name = mth.Name;
            classname = mth.DeclaringType.Name;
            _stopwatch.Start();
        }

        internal void StopTrace()
        {
            _stopwatch.Stop();
            time = _stopwatch.ElapsedMilliseconds;
        }

        internal void NewNestedMethod(MethodInfo methodTraceInfo)
        {
            _nestedStack.Add(methodTraceInfo);
        }
    }
}
