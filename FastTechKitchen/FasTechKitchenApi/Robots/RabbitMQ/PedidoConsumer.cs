using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Application.Interfaces;
using MassTransit;
using System.Text.Json;

// Define a mensagem que este consumer irá processar.
// Baseado no PedidoConsumerService antigo, a fila recebia um List<BasicPedido>.
// O MassTransit prefere mensagens fortemente tipadas.
// Assumindo que a mensagem do broker é de fato uma lista:

namespace FasTechKitchen.Api.Robots.RabbitMQ
{
    public class PedidoConsumer : IConsumer<List<BasicPedido>> // Altere <List<BasicPedido>> se a mensagem for outra
    {
        private readonly IPedidoApplicationService _pedidoAppService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PedidoConsumer> _logger;

        public PedidoConsumer(IServiceScopeFactory scopeFactory, ILogger<PedidoConsumer> logger, IPedidoApplicationService pedidoAppService)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _pedidoAppService = pedidoAppService;
        }

        public async Task Consume(ConsumeContext<List<BasicPedido>> context)
        {
            var pedidos = context.Message;

            _logger.LogInformation($"Recebido um lote de {pedidos.Count} pedidos.");

            try
            {
                // O MassTransit lida com a desserialização e injeção do escopo,
                // mas o IServiceScopeFactory garante que a injeção do ApplicationService seja resolvida
                // (visto que ele provavelmente tem dependências de DbContext, que são Scoped).
                using var scope = _scopeFactory.CreateScope();
                var pedidoAppService = scope.ServiceProvider.GetRequiredService<IPedidoApplicationService>();

                foreach (var pedido in pedidos)
                {
                    // Chama o método Add que está no seu PedidoApplicationService.
                    // O método Add retorna a entidade Pedido (com o ID gerado).
                    var pedidoSalvo = await _pedidoAppService.Add(pedido);

                    // Logamos o ID gerado pelo sistema.
                    _logger.LogInformation($"[MassTransit] Pedido Id {pedidoSalvo.Id} (Forma: {pedido.FormaDeEntrega}) processado com sucesso.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a mensagem do PedidoConsumer.");
                // O MassTransit irá tentar a reentrega por padrão.
                // Se falhar permanentemente, a mensagem vai para a fila de erro (DLQ).
                throw;
            }

            await Task.CompletedTask;
        }
    }
}
