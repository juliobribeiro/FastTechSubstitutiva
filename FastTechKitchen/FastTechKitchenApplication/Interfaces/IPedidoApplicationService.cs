using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Application.Interfaces.RabbitMQ;
using Humanizer;
using System.Linq.Expressions;
using EN = FastTechKitchen.Domain.Entities;
using DTO = FastTechKitchen.Application.DataTransferObjects;
using CTT = FastTech.Contracts.DataTransferObjects;
using MSG = FastTechKitchen.Application.DataTransferObjects.MessageBrokers;


namespace FastTechKitchen.Application.Interfaces;

public interface IPedidoApplicationService : IDisposable
{
    //Task<Pedido> GetById(Guid id);
    //Task<Pedido> Add(BasicPedido model);
    //Task<Pedido> Update(Pedido model);
    //Task<IEnumerable<Pedido>> FindBy(Expression<Func<EN.Pedido, bool>> expression);
    Task<DTO.Pedido> Add(CTT.PedidoMessage model);
   // Task<Pedido> Add(CTT.BasicPedido model);
    Task<DTO.Pedido> Update(DTO.Pedido model);
    Task<DTO.Pedido> Add(MSG.BasicPedido model);
    Task<IEnumerable<DTO.Pedido>> FindBy(Expression<Func<EN.Pedido, bool>> expression);
    Task<DTO.Pedido> Update(MSG.Pedido model);
    Task<DTO.Pedido> GetById(Guid id);

}