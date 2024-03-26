using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ConsolePart.Serializers;
using LibraryPart;
using Tracer_Formating.Utils.Serialization;

namespace ConsolePart
{
   
        public class Program
        {
            private static ITracer _tracer = new Tracer();
            private static Worker _worker1;
            private static Worker _worker2;

            public static void Main(string[] args)
            {
                // Начало трассировки
                _tracer.StartTrace();

                // Создание рабочих объектов
                _worker1 = new Worker(_tracer, "Worker1");
                _worker2 = new Worker(_tracer, "Worker2");

                // Выполнение методов
                _worker1.DoWork();
                _worker2.DoWork();

                // Завершение трассировки
                _tracer.StopTrace();

                // Вывод результатов
                DisplayResult();

                Console.ReadKey();
            }

        // Метод для вывода результатов трассировки в консоль
        static void DisplayResult()
        {
            ISerialize serializer;

            // Вывод результатов в консоль в формате XML
            serializer = new XML();
            Console.WriteLine("XML Result:");
            Console.WriteLine(serializer.Serialize(_tracer.GetTraceResult()));
            Console.WriteLine();

            // Вывод результатов в консоль в формате JSON
            serializer = new JSON();
            Console.WriteLine("JSON Result:");
            Console.WriteLine(serializer.Serialize(_tracer.GetTraceResult()));
        }
    }

    // Класс представляющий рабочего
    public class Worker
        {
            private ITracer _tracer;
            private string _name;

            public Worker(ITracer tracer, string name)
            {
                _tracer = tracer;
                _name = name;
            }

            // Метод для выполнения работы
            public void DoWork()
            {
                _tracer.StartTrace();

                // Выполнение работы
                Thread.Sleep(100);

                // Вызов вспомогательного метода
                DoSubWork();

                _tracer.StopTrace();
            }

            // Вспомогательный метод
            private void DoSubWork()
            {
                _tracer.StartTrace();

                // Выполнение вспомогательной работы
                Thread.Sleep(50);

                _tracer.StopTrace();
            }
        }
    
}
