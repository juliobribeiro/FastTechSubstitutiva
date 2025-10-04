using FastTechKitchen.Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

// Define o alias CTT
using CTT = FastTech.Contracts.DataTransferObjects;

namespace FasTechKitchen.Api.Robots.RabbitMQ
{
    // Consome a mensagem do Contrato
    public class PedidoConsumer : IConsumer<CTT.PedidoMessage>
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PedidoConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public PedidoConsumer(IServiceScopeFactory scopeFactory, ILogger<PedidoConsumer> logger, IPublishEndpoint publishEndpoint)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<CTT.PedidoMessage> context)
        {
            var mensagemCompleta = context.Message;
            var pedidosDoContrato = mensagemCompleta.Pedido;

            _logger.LogInformation($"[MassTransit] Recebido lote com {pedidosDoContrato.Count} itens para processamento.");

            using var scope = _scopeFactory.CreateScope();
            var pedidoAppService = scope.ServiceProvider.GetRequiredService<IPedidoApplicationService>();

            try
            {
                // Passa a mensagem completa para o ApplicationService para que ele itere e salve.
                var ultimoPedidoSalvo = await pedidoAppService.Add(mensagemCompleta);

                _logger.LogInformation($"[MassTransit] Lote de pedidos processado com sucesso. Último Pedido Id: {ultimoPedidoSalvo.Id}");

                // Publica a resposta de volta (Confirmação de Pedido Registrado)
                await _publishEndpoint.Publish(new CTT.PedidoRegistradoMessage
                {
                    // Usa a propriedade PedidoId
                    PedidoId = ultimoPedidoSalvo.Id,
                    // Usa a propriedade Status (Corrigido o erro CS0117)
                    Status = "GRAVADO_SUCESSO",
                    // Adiciona o CorrelationId da mensagem recebida
                    CorrelationId = context.CorrelationId.GetValueOrDefault()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a mensagem do PedidoConsumer.");

                // Opcional: Publicar uma mensagem de falha/erro.
                await _publishEndpoint.Publish(new CTT.PedidoRegistradoMessage
                {
                    PedidoId = Guid.Empty, // Ou o ID do pedido que causou a falha, se for possível isolar
                    Status = "FALHA_PROCESSAMENTO",
                    CorrelationId = context.CorrelationId.GetValueOrDefault()
                });

                // Re-lança para que o MassTransit possa fazer o re-try, se configurado.
                throw;
            }
        }
    }
}