using FastTech.Application.DataTransferObjects;
using FastTech.Application.Interfaces;
using MassTransit;

public class PedidoConsumerService : IConsumer<List<BasicPedido>>
{
    //private readonly IConfiguration _configuration;
    //private readonly IServiceScopeFactory _scopeFactory;

    private readonly IPedidoApplicationService _pedidoAppService;


    //public PedidoConsumerService(IConfiguration configuration, IServiceScopeFactory scopeFactory)
    public PedidoConsumerService(IPedidoApplicationService pedidoAppService)
    {
        //_configuration = configuration;
        //_scopeFactory = scopeFactory;
        _pedidoAppService = pedidoAppService;
    }

    public async Task Consume(ConsumeContext<List<BasicPedido>> context)
    {
        var pedidos = context.Message;
        if (pedidos is not null)
        {
            foreach (var pedido in pedidos)
            {
                await _pedidoAppService.Add(pedido);
            }
        }
    }

    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //{
    //    var rabbitConfig = _configuration.GetSection("RabbitMQ");

    //    var factory = new ConnectionFactory
    //    {
    //        HostName = rabbitConfig["HostName"],
    //        UserName = rabbitConfig["UserName"],
    //        Password = rabbitConfig["Password"],
    //        Port = int.TryParse(rabbitConfig["Port"], out var port) ? port : 5672
    //    };

    //    var connection = await factory.CreateConnectionAsync();
    //    var channel = await connection.CreateChannelAsync();

    //    var queueName = rabbitConfig["QueueName"];
    //    await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false);

    //    var consumer = new AsyncEventingBasicConsumer(channel);

    //    consumer.ReceivedAsync += async (sender, ea) =>
    //    {
    //        var body = ea.Body.ToArray();
    //        var message = Encoding.UTF8.GetString(body);

    //        try
    //        {
    //            var pedidos = JsonSerializer.Deserialize<List<BasicPedido>>(message, new JsonSerializerOptions
    //            {
    //                PropertyNameCaseInsensitive = true
    //            });

    //            if (pedidos is not null)
    //            {
    //                using var scope = _scopeFactory.CreateScope();
    //                var pedidoAppService = scope.ServiceProvider.GetRequiredService<IPedidoApplicationService>();

    //                foreach (var pedido in pedidos)
    //                    await pedidoAppService.Add(pedido);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
    //        }

    //        await Task.CompletedTask;
    //    };

    //    await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);

    //    await Task.Delay(Timeout.Infinite, stoppingToken);
    //}
}

