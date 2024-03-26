﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPart
{
   
        public interface ITracer
        {
            void StartTrace();
            void StopTrace();
            TraceResult GetTraceResult();
        
        }
}
