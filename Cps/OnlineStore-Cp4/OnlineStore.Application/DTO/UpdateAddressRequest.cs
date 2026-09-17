namespace OnlineStore.Application.DTO;

/// <summary>DTO para atualização parcial de endereço. Apenas os campos informados serão atualizados.</summary>
public record UpdateAddressRequest(string? Street, string? City, string? State, string? PostalCode, string? Number, string? Country);
