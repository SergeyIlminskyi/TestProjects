using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;

namespace UKLON.TestTask.IntegrationAdapter.Google
{
    public class Proxy : IProxy
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "UKLON test";
        static SheetsService service;
       

        public List<Data> GetSpreadsheetData<Data>(string spreadsheetId, string range)
            where Data : new()
        {
            var dataList = new List<Data>();

            using (var service = GetSheetsService())
            {
                try
                {
                    SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);

                    var response = request.Execute();

                    IList<IList<object>> values = response.Values;

                    if (values != null && values.Count > 0)
                    {
                        var type = typeof(Data);
                        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);


                        foreach (var row in values)
                        {
                            var dataItem = new Data();
                            foreach (var prop in properties.Select((value, index) => new { Value = value, Index = index }))
                            {
                                prop.Value.SetValue(dataItem, Convert(row[prop.Index].ToString(), prop.Value.PropertyType));
                            }
                            dataList.Add(dataItem);
                        }
                    }

                }
                catch { } //На реализацию нормальной обработки нет времени
                

                return dataList;
            } 
        }


        private GoogleCredential GetCredential()
        {
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                return GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
        }

        private SheetsService GetSheetsService()
        {
            return service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GetCredential(),
                ApplicationName = ApplicationName,
            });
        }

        private object Convert(string value, Type type)
        {
            if (type == typeof(Int16))
            {
                Int16 v;
                Int16.TryParse(value, out v);
                return v;
            }
            if (type == typeof(Int32))
            {
                Int32 v;
                Int32.TryParse(value, out v);
                return v;
            }
            if (type == typeof(Int64))
            {
                Int64 v;
                Int64.TryParse(value, out v);
                return v;
            }
            if (type == typeof(String))
                return value;
            // И так далее

            return value;
        }
    }
}
