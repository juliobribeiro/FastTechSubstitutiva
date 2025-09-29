namespace FastTech.Domain.Constants;

public class AppConstants
{
    public static class Policies
    {
        public const string Gerente = "Gerente";
        public const string Funcionario = "Funcionario";
        public const string Cliente = "Cliente";
    }

    public static class Routes
    {
        public static class RabbitMQ
        {
            public const string ItemCardapioInsert = "ItemCardapio.insert";
            public const string ItemCardapioUpdate = "ItemCardapio.update";

            public const string PedidoInsert = "Pedido.insert";
            public const string PedidoUpdate = "Pedido.update";
        }
    }
}