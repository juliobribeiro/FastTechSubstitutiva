using FastTech.Application.DataTransferObjects;
using FastTech.Application.Interfaces;
using MassTransit;

//public class PedidoConsumerService : IConsumer<List<BasicPedido>>
public class PedidoConsumerService : IConsumer<PedidoMessage>
{
    //private readonly IConfiguration _configuration;
    //private readonly IServiceScopeFactory _scopeFactory;

    private readonly IPedidoApplicationService _pedidoAppService;
    private readonly ILogger<PedidoConsumerService> _logger;


    //public PedidoConsumerService(IConfiguration configuration, IServiceScopeFactory scopeFactory)
    public PedidoConsumerService(IPedidoApplicationService pedidoAppService, ILogger<PedidoConsumerService> logger)
    {
        //_configuration = configuration;
        //_scopeFactory = scopeFactory;
        _pedidoAppService = pedidoAppService;
        _logger = logger;
    }

    //public async Task Consume(ConsumeContext<List<BasicPedido>> context)
    public async Task Consume(ConsumeContext<PedidoMessage> context)
    {
        var mensagemRecebida = context.Message;

        // Log para rastrear
        _logger.LogInformation("Mensagem de Pedido recebida. Total de itens: {Count}",
            mensagemRecebida.Pedido?.Count ?? 0);

        if (mensagemRecebida.Pedido == null || !mensagemRecebida.Pedido.Any())
        {
            _logger.LogWarning("Mensagem recebida sem itens de pedido. Ignorando.");
            return;
        }

        // Lógica de Negócio: Processa cada BasicPedido contido na mensagem.
        foreach (var item in mensagemRecebida.Pedido)
        {
            try
            {
                // Chama o Application Service para adicionar o pedido ao banco de dados ou processá-lo.
                var novoPedido = await _pedidoAppService.Add(item);

                _logger.LogInformation("Pedido processado com sucesso. ID do Novo Pedido: {Id}", novoPedido.Id);
            }
            catch (Exception ex)
            {
                // É crucial lidar com exceções para decidir se a mensagem deve ser repetida (retry)
                // ou enviada para uma fila de erro (error queue).
                _logger.LogError(ex, "Falha ao processar o pedido {Item}. Razão: {Message}",
                    System.Text.Json.JsonSerializer.Serialize(item), ex.Message);

                // Em um ambiente de produção, você pode relançar a exceção para o MassTransit 
                // lidar com o Retry/Dead-Letter Queue (DLQ), ou tratar o erro de outra forma.
                // throw; 
            }
        }

        //foreach (var item in mensagemRecebida.Pedido)
        //{
        //    await _pedidoAppService.Add(pedido);
        //}

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

