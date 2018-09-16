using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace Moneyveo.TestTask.Helpers
{
    public class CSVParser
    {
        #region Export CVS
        public static void ExportCSV(IMatrixModel matrix, System.IO.StreamWriter file, char separatorCVS)
        {
            for (int i = 0; i < matrix.Size; i++)
            {
                file.WriteLine(string.Join(separatorCVS.ToString(), matrix.Body[i]));
            }
        }
        #endregion

        #region Import CVS
        public static int[][] ImportCSV(HttpPostedFileBase file, char separator)
        {

            var body = new List<int[]>();
            string lastProcessedProperty = String.Empty;
            try
            {
                MemoryStream stream = new MemoryStream();
                file.InputStream.CopyTo(stream);
                StreamReader sr = new StreamReader(stream, Encoding.UTF8);

                string aa = sr.ReadToEnd();
                stream.Seek(0, SeekOrigin.Begin);
                string[] lineArray;

                string line;

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine() ?? string.Empty;
                    lineArray = line.Split(new char[] { separator }, StringSplitOptions.None);

                    var arrayInt = new int[lineArray.Length];
                    for (int i = 0; i < lineArray.Length; i++)
                    {
                        arrayInt[i] = Int32.Parse(lineArray[i]);
                    }

                    body.Add(arrayInt);
                }

            }
            catch (Exception)
            {
                throw new Exception("Error of file parsing! Check, that file is filled correctly.");
            }

            return body.ToArray();
        }
        #endregion

    }
}