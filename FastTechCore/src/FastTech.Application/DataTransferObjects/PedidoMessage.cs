using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Application.DataTransferObjects
{
    public class PedidoMessage
    {
        public List<BasicPedido> Pedido { get; set; } = new List<BasicPedido>();
    }
}
