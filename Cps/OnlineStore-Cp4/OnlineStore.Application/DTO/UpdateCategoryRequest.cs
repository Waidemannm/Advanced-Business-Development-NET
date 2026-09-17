namespace OnlineStore.Application.DTO;

/// <summary>DTO para atualização parcial de categoria. Apenas os campos informados serão atualizados.</summary>
public record UpdateCategoryRequest(string? Name, string? Description);
