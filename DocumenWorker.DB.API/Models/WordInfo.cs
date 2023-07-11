using DocumenWorker.DB.API.Domains;
using DocumenWorker.DB.API.Domains.Interfaces;
using DocumenWorker.DB.API.Models.Interfaces;
using DocumenWorker.DB.API.Repository.Interfaces;

namespace DocumenWorker.DB.API.Models
{
    public class WordInfo : WordInfoDomain, ICorrelated
    {
        public Guid CorrelationId { get; private set; }
        public WordInfo WithCorrelationId(Guid correlationId)
        {
            var wordInfo = (WordInfo)MemberwiseClone();
            wordInfo.CorrelationId = correlationId;
            return wordInfo;
        }
    }
}
