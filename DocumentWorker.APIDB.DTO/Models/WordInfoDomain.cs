using DocumentWorker.APIDB.DTO.Models.Interfaces;

namespace DocumentWorker.APIDB.DTO.Models
{
    /// <summary>
    /// Модель основной таблицы
    /// </summary>
    public class WordInfoDomain : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
