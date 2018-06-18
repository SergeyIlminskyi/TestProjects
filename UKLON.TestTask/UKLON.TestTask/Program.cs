using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKLON.TestTask.IntegrationAdapter;
using UKLON.TestTask.IntegrationAdapter.Yandex;

namespace UKLON.TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            { 
                Console.WriteLine("Write region id (Moscow - 213, Kyiv - 143, Chelyabinsk - 56):");

                var id = Console.ReadLine();
                var fileWorker = new FileWorker();
                var proxy = new Proxy(fileWorker);

                var res = proxy.GetRegionTrafficInfo(Convert.ToInt32(id));

                if(res.Id > 0)
                    Console.WriteLine(res.FormatData());
                else
                    Console.WriteLine("Not found");

                Console.WriteLine();

            } while (true); //С интерфейсом и валидацией не заморачивался, поскольку в ТЗ такая задача поставлена не была.
            
        }
    }
}
