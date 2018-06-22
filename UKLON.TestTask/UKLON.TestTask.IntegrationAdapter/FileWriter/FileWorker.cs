﻿using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;

namespace UKLON.TestTask.IntegrationAdapter
{
    public class FileWorker : IFileWorker
    {
        private static string filePath = ConfigurationSettings.AppSettings["file-path"];

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
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.Default))
                {
                    foreach(var data in dataList)
                    {
                        sw.WriteLine(data.FormatData());
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
