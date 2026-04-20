using OnlineSore.Domain.Entities;
using OnlineSore.Domain.Enum;

namespace OnlineStore.Application.Interfaces;

/// <summary>
/// Interface do serviço de cliente.
/// Define as operações CRUD e consultas disponíveis para a entidade Costumer.
/// </summary>
public interface ICostumerService
{
    /// <summary>
    /// Cria um novo cliente no sistema.
    /// </summary>
    /// <param name="costumer">Entidade Costumer com os dados do cliente a ser criado.</param>
    /// <returns>O cliente criado com Id e CreatedAt preenchidos.</returns>
    /// <exception cref="InvalidOperationException">Lançada quando já existe cliente com o mesmo e-mail.</exception>
    Costumer CreateCostumer(Costumer costumer);   
    
    /// <summary>
    /// Busca um cliente pelo seu identificador único.
    /// </summary>
    /// <param name="id">Guid do cliente.</param>
    /// <returns>O cliente encontrado ou null se não existir.</returns>
    Costumer? GetById(Guid id);
    
    /// <summary>
    /// Busca um cliente pelo endereço de e-mail.
    /// </summary>
    /// <param name="email">E-mail do clientee</param>
    /// <returns>O cliente encontrado ou null se não existir.</returns>
    Costumer? GetCostumerByEmail(string email);
    
    /// <summary>
    /// Retorna todos os clientes ativos do sistema.
    /// </summary>
    /// <returns>Lista de clientes.</returns> 
    IReadOnlyList<Costumer> GetAll();
    
  
    /// <summary>
    /// Atualiza os dados de um cliente existente.
    /// </summary>
    /// <param name="id">Id do cliente a ser atualizado.</param>
    /// <param name="updateName">Novo nome.</param>
    /// <param name="updateBirthDate">Nova data de nascimento.</param>
    /// /// <param name="updateEmail">Novo e-mail.</param>
    /// <returns>O cliente atualizado.</returns>
    /// <exception cref="InvalidOperationException">Lançada quando o cliente não existe ou o e-mail já está em uso.</exception>
    Costumer UpdateCostumer(Guid id, string? updateName, DateOnly? updateBirthDate, string? updateEmail);
    
    /// <summary>
    /// Verifica se existe um cliente com o e-mail informado.
    /// </summary>
    /// <param name="email">E-mail a ser verificado.</param>
    /// <param name="excludeCostumerId">Id de cliente a excluir da verificação (útil ao atualizar - evita conflito com o próprio cliente).</param>
    /// <returns>True se o e-mail já está em uso; false caso contrário.</returns>
    bool EmailExists(string email, Guid? excludeCostumerId = null);
    
    /// <summary>
    /// Remove um cliente pelo Id.
    /// </summary>
    /// <param name="id">Id do cliente a ser removido.</param>
    /// <exception cref="InvalidOperationException">Lançada quando o cliente não existe.</exception>
    void DeleteCostumer(Guid id);
}
    