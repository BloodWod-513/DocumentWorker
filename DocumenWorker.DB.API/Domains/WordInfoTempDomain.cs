using DocumenWorker.DB.API.Domains.Interfaces;

namespace DocumenWorker.DB.API.Domains
{
    public class WordInfoTempDomain : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool IsNew { get; set; }
    }
}
