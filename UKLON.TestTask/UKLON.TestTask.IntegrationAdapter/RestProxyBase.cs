using System;
using System.Net;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using System.Xml.Serialization;
using System.IO;

namespace UKLON.TestTask.IntegrationAdapter
{
    public class RestProxyBase
    {

        internal ResultResponse Invoke<TResponse>(string requestUri, string methodName, out TResponse response)
            where TResponse : BaseYandexResponse, new()
        {
            var result = new ResultResponse(Result.Success, YandexMappingResult.MappingYandexResult);
            response = new TResponse();

            try
            {
                using (var client = new HttpClient())
                {

                  
                    var baseAddress = ConfigurationSettings.AppSettings["yandex-api-address"];
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                    //logger.Info("ESB REQUEST " + methodName + " URL: " + baseAddress + requestUri);

                    var UrlResponse = client.GetAsync(requestUri).Result;
                    if (UrlResponse.IsSuccessStatusCode)
                    {
                        string responseHttpClient = UrlResponse.Content.ReadAsStringAsync().Result;

                        //logger.Info("ESB RESPONSE " + methodName + responseHttpClient);

                        response = DeserializeXml<TResponse>(responseHttpClient);
                    }
                    else
                    {
                        result = new ResultResponse(Result.UnknownError, YandexMappingResult.MappingYandexResult);
                    }
                }
            }
            catch (TimeoutException ex)
            {
                //logger.Error(ex);
                result = new ResultResponse(Result.TimeoutError, YandexMappingResult.MappingYandexResult);
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                result = new ResultResponse(Result.UnknownError, YandexMappingResult.MappingYandexResult);
            }

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
        public static TResult Throw<TResult>(this TResult response) where TResult : ResultResponse, new()
        {
            if (!response.IsSuccess)
                throw new Exception();

            return response;
        }
    }
}
