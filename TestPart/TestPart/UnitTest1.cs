using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using FluentAssertions;
using LibraryPart;

namespace TestPart
{
    [TestClass]
    public class UnitTest1
    {
        private static ITracer _tracer;
        private static A _A;
        private static B _B;
        private static int[] ids;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _tracer = new Tracer();
            _tracer.StartTrace();
            Thread thread1 = new Thread(M2);
            Thread thread2 = new Thread(M2);
            _A = new A(_tracer);
            _B = new B(_tracer);
            _A.MethodA();
            _B.MethodB();

            thread1.Start();
            thread2.Start();
            _tracer.StopTrace();
            thread1.Join();
            thread2.Join();
            _tracer.GetTraceResult();
            ids = _tracer.GetTraceResult()._threadList.Keys.ToArray<int>();
        }

        [TestMethod]
        public void TestMethodNames()
        {
            _tracer.GetTraceResult()._threadList[ids[2]]._methods[0]._nestedStack[1].name.Should().Be("MethodB");
            _tracer.GetTraceResult()._threadList[ids[0]]._methods[0]._nestedStack[0]._nestedStack[0].name.Should().Be("MethodA1");
            _tracer.GetTraceResult()._threadList[ids[1]]._methods[0].name.Should().Be("M2");
        }

        [TestMethod]
        public void TestMethodClasses()
        {
            _tracer.GetTraceResult()._threadList[ids[2]]._methods[0]._nestedStack[1].classname.Should().Be("B");
        }

        [TestMethod]
        public void TestExecutionTime()
        {
            _tracer.GetTraceResult()._threadList[ids[1]]._methods[0].time.Should().BeGreaterThan(_tracer.GetTraceResult()._threadList[ids[1]]._methods[0]._nestedStack[0].time);
            _tracer.GetTraceResult()._threadList[ids[0]]._methods[0].time.Should().Be(_tracer.GetTraceResult()._threadList[ids[0]]._thread_time);
        }

        [TestMethod]
        public void TestCountThread()
        {
            _tracer.GetTraceResult()._threadList.Count.Should().Be(3);
        }

        private static void M2()
        {
            _tracer.StartTrace();
            _A.MethodA();
            Thread.Sleep(200);
            _B.MethodB();
            _tracer.StopTrace();
        }
    }

    public class A
    {
        private ITracer _trace;
        private B _B;
        public static bool fl = false;
        public A(ITracer trace)
        {
            _trace = trace;
            _B = new B(_trace);
        }
        public void MethodA()
        {
            _trace.StartTrace();
            if (!fl)
                MethodA1();
            _B.MethodB();
            Thread.Sleep(30);
            _trace.StopTrace();
        }

        public void MethodA1()
        {
            _trace.StartTrace();
            for (int i = 0; i < 3; i++)
            {
                fl = true;
                MethodA();
            }
            Thread.Sleep(30);
            _trace.StopTrace();
        }
    }

    public class B
    {
        private ITracer _trace;
        public B(ITracer trace)
        {
            _trace = trace;
        }
        public void MethodB()
        {
            _trace.StartTrace();
            Thread.Sleep(40);
            _trace.StopTrace();
        }
    }
}
