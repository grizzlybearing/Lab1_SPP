using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace LibraryPart
{
    
    public class Tracer: ITracer
    {
        private TraceResult _traceResult;
        private static readonly object SyncRoot = new object();
        public Tracer()
        {
            _traceResult = new TraceResult();
        }

        // вызывается в начале замеряемого метода
        public void StartTrace()
        {
            StackTrace _stackTrace = new StackTrace(1);
            StackFrame _frame = _stackTrace.GetFrame(0);
            MethodBase _method = _frame.GetMethod();
            _traceResult.StartTrace(Thread.CurrentThread.ManagedThreadId, _method);
        }

      

        // вызывается в конце замеряемого метода 
        public void StopTrace()
        {
            _traceResult.StopTrace(Thread.CurrentThread.ManagedThreadId);
        }


        // получить результаты измерений  
    
        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }
    }
}
