using System;
using System.Net;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using UKLON.TestTask.Structs;

namespace UKLON.TestTask.IntegrationAdapter
{
    public class HttpProxyBase
    {

        internal TResponse Invoke<TResponse>(string requestUri, out  ResultResponse result)
            where TResponse : new()
        {

            var response = new TResponse();

            try
            {
                using (var client = new HttpClient())
                {
                  
                    var baseAddress = ConfigurationSettings.AppSettings["yandex-api-address"];
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                    var UrlResponse = client.GetAsync(requestUri).Result;

                    result = Handle((int)UrlResponse.StatusCode, UrlResponse.StatusCode.ToString(), YandexMappingResult.MappingYandexResult);// Не имея документации, будем считать, что логика обработки заключается только в анализе http-кодов

                    
                    if (result.IsSuccess)
                    {
                        string responseHttpClient = UrlResponse.Content.ReadAsStringAsync().Result;
                        response = DeserializeXml<TResponse>(responseHttpClient);
                    }
                    
                }
            }
            catch (TimeoutException ex)
            {
                result = Handle(Result.TimeoutError, ex);
            }
            catch (Exception ex)
            {
                result = Handle(Result.UnknownError, ex);
            }

            return response;
        }

        internal List<RegionTrafficInfoWithStatus> GetRegionsList(List<RegionData> regionData) //Можно красивее придумать, но у меня нет времени
        {
            var response = new List<RegionTrafficInfoWithStatus>();

            using (var client = new HttpClient())
            {
                var baseAddress = ConfigurationSettings.AppSettings["yandex-api-address"];
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                foreach(var data in regionData)
                {

                    var result = new ResultResponse();
                    var item = new RegionTrafficInfoWithStatus();
                    try
                    {
                        var UrlResponse = client.GetAsync(String.Format("reginfo.xml?region={0}&lang={1}" , data.Id, "en")).Result;

                        result = Handle((int)UrlResponse.StatusCode, UrlResponse.StatusCode.ToString(), YandexMappingResult.MappingYandexResult);

                        if (result.IsSuccess)
                        {
                            string responseHttpClient = UrlResponse.Content.ReadAsStringAsync().Result;
                            item = (RegionTrafficInfoWithStatus)DeserializeXml<FullRegionInfo>(responseHttpClient);
                            
                        }
                    }
                    catch (TimeoutException ex)
                    {
                        result = Handle(Result.TimeoutError, ex);
                        
                    }
                    catch (Exception ex)
                    {
                        result = Handle(Result.UnknownError, ex);
                    }

                    item.Code = result.ExternalCode;
                    item.Text = result.ExternalText;

                    if (!result.IsSuccess)
                        item.Id = data.Id;

                    response.Add(item);
                }
            }

            return response;
        }




        private ResultResponse Handle(Result result, Exception ex)
        {
            return new ResultResponse()
            {
                ExecutionResult = result,
                ExternalText = ex.ToString(),
                ExternalCode = "---"
            };
        }


        private ResultResponse Handle(int responseCode, string responseText, Dictionary<int, Result> customMapping)
        {

            Result mappedResult;

            var result = new ResultResponse()
            {
                ExternalCode = responseCode.ToString(),
                ExternalText = responseText
            };

            if (customMapping.TryGetValue(responseCode, out mappedResult))
                result.ExecutionResult = mappedResult;
            else
                result.ExecutionResult = Result.UnknownError;


            return result;
        }

        private  T DeserializeXml<T>(string content)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(content))
                return (T)serializer.Deserialize(sr);
        }
    }

    public static class ResultResponseExtensions
    {
        public static TResult Throw<TResult>(this TResult result) where TResult : ResultResponse, new()
        {
            if (!result.IsSuccess)
                throw new Exception();

            return result;
        }
    }
}
