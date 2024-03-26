using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPart
{
    public class ThreadInfo
    {
        private Stack<MethodInfo> _nestedStack = new Stack<MethodInfo>();
        public List<MethodInfo> _methods = new List<MethodInfo>();
        public long _thread_time;

        internal void StartTrace(MethodInfo mth)
        {
            if (_nestedStack.Count == 0)
            {
                _methods.Add(mth);
            }
            else
            {
                _nestedStack.Peek().NewNestedMethod(mth);
            }
            _nestedStack.Push(mth);
        }

        internal void StopTrace()
        {
            _nestedStack.Pop().StopTrace();
            _thread_time = _methods[0].time;
        }
    }
}
