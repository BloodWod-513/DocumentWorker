using DocumenWorker.DB.API.Domains.Interfaces;

namespace DocumenWorker.DB.API.Domains
{
    public class WordInfoDomain : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
