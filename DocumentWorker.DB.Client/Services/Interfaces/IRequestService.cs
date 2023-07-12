using DocumentWorker.APIDB.DTO.Models;
using DocumentWorker.APIDB.DTO.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.APIDB.Client.Services.Interfaces
{
    public interface IRequestService<T> where T : IBaseEntity
    {
        public string ControllerName { get; }
        public bool AddRequest(T model);
        public bool AddRangeRequest(List<T> model);
        public T GetRequest(int id);
    }
}
