namespace OnlineStore.Application.DTO;

/// <summary>
/// DTO para criação de novo pedido.
/// Recebido no body das requisições POST /api/products.
/// </summary>
/// <param name="IdCategory">Identificador da categoria .</param>
/// <param name="Name">Nome do cliente.</param>
/// <param name="Description">Descrição do produto.</param>
/// <param name="Price">Preço do produto.</param>
/// <param name="Stock">Quantidade de itens disponíveis em estoque.</param>
public record UpdateProductRequest(Guid? IdCategory, string? Name, string? Description, decimal? Price, int? Stock);