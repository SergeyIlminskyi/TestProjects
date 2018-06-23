using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Threading;

namespace UKLON.TestTask.IntegrationAdapter
{
    public class FileWorker : IFileWorker
    {
        private static string filePath = ConfigurationSettings.AppSettings["file-path"];
        static object locker = new object();
        public void WriteToFile<Data, Result>(Data data, Result result) 
               where Result : IResponse
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(String.Format("{0}     || Code = {1} | Text = {2}", data.FormatData() ,result.ResponseCode, result.ResponseText));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void WriteListToFile<Data>(List<Data> dataList) 
        {

            lock (locker)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.Default))
                    {
                        foreach (var data in dataList)
                        {
                            sw.WriteLine(data.FormatData());
                        }

                    }
                    Console.WriteLine(String.Format("Thread with id={0} was write!", Thread.CurrentThread.ManagedThreadId));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
