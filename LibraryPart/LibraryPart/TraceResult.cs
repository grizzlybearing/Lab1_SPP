using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPart
{
    public class TraceResult
    {
        public ConcurrentDictionary<int, ThreadInfo> _threadList = new ConcurrentDictionary<int, ThreadInfo>();

        internal void StartTrace(int id, MethodBase mth)
        {
            ThreadInfo threadInfo = _threadList.GetOrAdd(id, new ThreadInfo());
            threadInfo.StartTrace(new MethodInfo(mth));
        }

        internal void StopTrace(int id)
        {
            ThreadInfo threadInfo;
            if (!_threadList.TryGetValue(id, out threadInfo))
            {
                throw new Exception("no such id");
            }
            threadInfo.StopTrace();
        }
    }
}
