using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using UKLON.TestTask.Structs;
using UKLON.TestTask.IntegrationAdapter;
using Y = UKLON.TestTask.IntegrationAdapter.Yandex;
using G = UKLON.TestTask.IntegrationAdapter.Google;

namespace UKLON.TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            { 
                Console.WriteLine("Start! Please wait...");

 
                var fileWorker = new FileWorker();

                var yproxy = new Y.Proxy(fileWorker);
                var gproxy = new G.Proxy();


                var stopwatch = Stopwatch.StartNew();

                var regions  = gproxy.GetSpreadsheetData<RegionData>("1AhemqFP2lZ4ifcXGmArOydA3w24Yd7LdQ3KZveN-JR4", "A2:B");

                //foreach (var region in regions)
                //{
                //    yproxy.GetRegionTrafficInfo(Convert.ToInt32(region.Id));// В консоль не выводим, так как пишется в файл
                //}

                //var result = yproxy.GetListRegionTrafficInfo(regions);

                yproxy.WriteListRegionTrafficInfo(regions); //Многопоточность особо ничего не решит, поскольку потоки будут ждать пока освободится файл

                Console.WriteLine(stopwatch.Elapsed.ToString());
                Console.WriteLine("Finish!");

                Console.WriteLine();

            } while (true); //С интерфейсом и валидацией не заморачивался, поскольку в ТЗ такая задача поставлена не была.
            
        }
    }
}
