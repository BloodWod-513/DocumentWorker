using DocumentWorker.APIDB.DTO.Models.Interfaces;

namespace DocumentWorker.APIDB.DTO.Models
{
    /// <summary>
    /// Модель второстепенной таблицы
    /// </summary>
    public class WordInfoTempDomain : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool IsNew { get; set; }
    }
}
