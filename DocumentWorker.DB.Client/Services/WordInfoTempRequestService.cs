using DocumentWorker.APIDB.Client.Services.Interfaces;
using DocumentWorker.APIDB.DTO.Models;
using DocumentWorker.APIDB.DTO.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace DocumentWorker.APIDB.Client.Services
{
    /// <summary>
    /// Класс, в котором описывается в какой контроллер API DB пойдут запросы
    /// </summary>
    /// <typeparam name="T">Объект, с короым не обходимо работать</typeparam>
    public class WordInfoTempRequestService<T> : BaseRequestService<T> where T : WordInfoTempDomain
    {
        public override string ControllerName => "/api/WordInfoTemp";
        public string Url { get; private set; }
        public WordInfoTempRequestService(string url) : base(url) { }
    }
}
