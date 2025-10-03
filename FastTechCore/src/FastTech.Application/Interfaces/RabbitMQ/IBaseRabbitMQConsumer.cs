namespace FastTech.Application.Interfaces.RabbitMQ
{
    public interface IBaseRabbitMQConsumer : IDisposable
    {
        Task Consumer(string message, string rountingKey);
        Task PublishAsync(object message, string rountingKey);
    }
}