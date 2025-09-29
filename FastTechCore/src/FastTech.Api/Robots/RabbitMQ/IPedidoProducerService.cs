public interface IPedidoProducerService
{
    Task PublishMessageAsync(string message);
}