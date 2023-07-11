using DocumenWorker.DB.API.Domains;
using DocumenWorker.DB.API.Domains.Interfaces;
using DocumenWorker.DB.API.Models.Interfaces;

namespace DocumenWorker.DB.API.Models
{
    public class WordInfoTemp : WordInfoTempDomain, ICorrelated
    {
        public Guid CorrelationId { get; private set; }
        public WordInfoTemp WithCorrelationId(Guid correlationId)
        {
            var wordInfoTemp = (WordInfoTemp)MemberwiseClone();
            wordInfoTemp.CorrelationId = correlationId;
            return wordInfoTemp;
        }
    }
}
