namespace FastTechKitchen.Application.DataTransferObjects.MessageBrokers;

public record BasicItemCardapio(
    string Nome,
    string Descricao,
    double Preco,
    bool Disponivel
);