using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTO;
using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;

namespace OnlineStore.API.Controllers;

/// <summary>
/// Gerenciamento de categorias de produtos.
/// <br/>Este controller demonstra o uso direto do <see cref="IRepository{T}"/> genérico (sem serviço intermediário).
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoryController : ControllerBase
{
    private readonly IRepository<Category> _repository;

    public CategoryController(IRepository<Category> repository) =>
        _repository = repository;

    /// <summary>Retorna todas as categorias cadastradas.</summary>
    /// <response code="200">Lista de categorias retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var list = await _repository.GetAllAsync();
        return Ok(list.Select(CategoryResponse.FromDomain));
    }

    /// <summary>Busca uma categoria pelo identificador único.</summary>
    /// <param name="id">GUID da categoria.</param>
    /// <response code="200">Categoria encontrada.</response>
    /// <response code="404">Categoria não encontrada.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null) return NotFound();
        return Ok(CategoryResponse.FromDomain(category));
    }

    /// <summary>Cria uma nova categoria.</summary>
    /// <remarks>
    /// Exemplo de payload:
    /// <code>
    /// {
    ///   "name": "Eletrônicos",
    ///   "description": "Smartphones, notebooks e acessórios"
    /// }
    /// </code>
    /// </remarks>
    /// <param name="request">Dados da categoria a ser criada.</param>
    /// <response code="201">Categoria criada com sucesso.</response>
    /// <response code="400">Dados inválidos (ex.: nome vazio ou muito longo).</response>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        var category = request.ToDomain();
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, CategoryResponse.FromDomain(category));
    }

    /// <summary>Atualiza o nome e/ou descrição de uma categoria existente.</summary>
    /// <param name="id">GUID da categoria.</param>
    /// <param name="request">Campos a serem atualizados (parcial).</param>
    /// <response code="200">Categoria atualizada.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Categoria não encontrada.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var category = await _repository.GetByIdAsync(id)
            ?? throw new ResourceNotFoundException("Categoria", id);

        if (request.Name is not null) category.UpdateName(request.Name);
        if (request.Description is not null) category.UpdateDescription(request.Description);

        await _repository.SaveChangesAsync();
        return Ok(CategoryResponse.FromDomain(category));
    }

    /// <summary>Remove uma categoria pelo identificador único.</summary>
    /// <param name="id">GUID da categoria.</param>
    /// <response code="204">Categoria removida.</response>
    /// <response code="404">Categoria não encontrada.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var category = await _repository.GetByIdAsync(id)
            ?? throw new ResourceNotFoundException("Categoria", id);
        _repository.Delete(category);
        await _repository.SaveChangesAsync();
        return NoContent();
    }
}

