using DocumentWorker.APIDB.Client.Services.Interfaces;
using DocumentWorker.APIDB.DTO.Models;
using DocumentWorker.APIDB.DTO.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.APIDB.Client.Services
{
    /// <summary>
    /// Класс в котором описано, как общаться с API BD
    /// </summary>
    /// <typeparam name="T">Объект, с которым необходимо работать</typeparam>
    public abstract class BaseRequestService<T> : IRequestService<T> where T : class, IBaseEntity
    {
        public virtual string ControllerName => "";
        public string Url { get; protected set; }
        public BaseRequestService(string url)
        {
            Url = url;
        }
        public bool AddRangeRequest(List<T> wordInfoDomain)
        {
            using (var httpClient = new HttpClient())
            {
                var zxc = JsonConvert.SerializeObject(wordInfoDomain);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(wordInfoDomain), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{Url + ControllerName}/AddRange"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var resultString = SendAsync(httpClient, request);

                return bool.Parse(resultString);
            }
        }
        public bool AddRequest(T wordInfoDomain)
        {
            using (var httpClient = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(wordInfoDomain), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{Url + ControllerName}"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var resultString = SendAsync(httpClient, request);

                return bool.Parse(resultString);
            }
        }

        public T GetRequest(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{Url + ControllerName}/{id}"),
                    Method = HttpMethod.Get,
                };
                var resultString = SendAsync(httpClient, request);

                var result = JsonConvert.DeserializeObject<WordInfoTempDomain>(resultString) as T;

                return result;
            }
        }
        private string SendAsync(HttpClient httpClient, HttpRequestMessage request)
        {
            var task = httpClient.SendAsync(request).Result;
            return task.Content.ReadAsStringAsync().Result;
        }
    }
}
