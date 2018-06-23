using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
 
            Console.WriteLine("Start! Please wait...");
            var fileWorker = new FileWorker();

            bool threadsIsAlive = false;

            var stopwatch = Stopwatch.StartNew();
            var gproxy = new G.Proxy();
            var regions  = gproxy.GetSpreadsheetData<RegionData>("1AhemqFP2lZ4ifcXGmArOydA3w24Yd7LdQ3KZveN-JR4", "A2:B");
            
            var regionParts = regions.Split(8);

            List<Thread> threadList = new List<Thread>();

            foreach (var regionPart in regionParts)
            {
                var yproxy = new Y.Proxy(fileWorker);
                yproxy.dataList = regionPart.ToList();
                Thread thread = new Thread(new ThreadStart(yproxy.WriteListRegionTrafficInfo));
                threadList.Add(thread);
                thread.Start();

                Console.WriteLine(String.Format("Thread with id={0} started!", thread.ManagedThreadId));
            }

            do
            {
                threadsIsAlive = false;
                foreach (var item in threadList)
                {
                    if (item.IsAlive)
                        threadsIsAlive = true;
                }
            }
            while (threadsIsAlive); //Для подсчета времени выполнения

            //foreach (var region in regions)
            //{
            //    yproxy.GetRegionTrafficInfo(Convert.ToInt32(region.Id));// В консоль не выводим, так как пишется в файл
            //}

            //var result = yproxy.GetListRegionTrafficInfo(regions);

            //yproxy.WriteListRegionTrafficInfo(regions);     

            Console.WriteLine(stopwatch.Elapsed.ToString());
            Console.WriteLine("Finish!");

            Console.ReadKey();   
        }
    }
}
