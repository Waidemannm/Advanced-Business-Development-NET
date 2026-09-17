namespace OnlineStore.Application.DTO;

/// <summary>DTO para atualização parcial de cliente. Apenas os campos informados serão atualizados.</summary>
public record UpdateCostumerRequest(string? Name, DateOnly? BirthDate, string? Email);
