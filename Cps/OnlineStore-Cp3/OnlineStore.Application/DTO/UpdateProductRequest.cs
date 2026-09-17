namespace OnlineStore.Application.DTO;

/// <summary>DTO para atualização parcial de produto. Apenas os campos informados serão atualizados.</summary>
public record UpdateProductRequest(Guid? IdCategory, string? Name, string? Description, decimal? Price, int? Stock);
