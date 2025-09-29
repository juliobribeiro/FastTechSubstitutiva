using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

public class PedidoConsumerService //: BackgroundService
{
    //private readonly IConfiguration _configuration;
    //private readonly IServiceScopeFactory _scopeFactory;

    //public PedidoConsumerService(IConfiguration configuration, IServiceScopeFactory scopeFactory)
    //{
    //    _configuration = configuration;
    //    _scopeFactory = scopeFactory;
    //}

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

