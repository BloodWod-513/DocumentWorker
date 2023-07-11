namespace DocumenWorker.DB.API.Models.Interfaces
{
    public interface ICorrelated
    {
        Guid CorrelationId { get; }
    }
}
