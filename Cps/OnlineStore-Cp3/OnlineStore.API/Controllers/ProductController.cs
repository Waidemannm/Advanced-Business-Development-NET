using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTO;
using OnlineStore.Application.Interfaces;

namespace OnlineStore.API.Controllers;

/// <summary>Gerenciamento do catálogo de produtos.</summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService) =>
        _productService = productService;

    /// <summary>Retorna todos os produtos do catálogo.</summary>
    /// <response code="200">Lista de produtos retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var list = _productService.GetAll();
        return Ok(list.Select(ProductResponse.FromDomain));
    }

    /// <summary>Busca um produto pelo identificador único.</summary>
    /// <param name="id">GUID do produto.</param>
    /// <response code="200">Produto encontrado.</response>
    /// <response code="404">Produto não encontrado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var product = _productService.GetById(id);
        if (product is null) return NotFound();
        return Ok(ProductResponse.FromDomain(product));
    }

    /// <summary>Busca um produto pelo nome.</summary>
    /// <param name="name">Nome do produto.</param>
    /// <response code="200">Produto encontrado.</response>
    /// <response code="404">Produto não encontrado.</response>
    [HttpGet("name/{name}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult GetByName(string name)
    {
        var product = _productService.GetProductByName(name);
        if (product is null) return NotFound();
        return Ok(ProductResponse.FromDomain(product));
    }

    /// <summary>Cria um novo produto no catálogo.</summary>
    /// <param name="request">Dados do produto a ser criado.</param>
    /// <response code="201">Produto criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] CreateProductRequest request)
    {
        var product = _productService.CreateProduct(request.ToDomain());
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ProductResponse.FromDomain(product));
    }

    /// <summary>Atualiza os dados de um produto existente.</summary>
    /// <param name="id">GUID do produto.</param>
    /// <param name="request">Campos a serem atualizados (parcial).</param>
    /// <response code="200">Produto atualizado.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Produto não encontrado.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult Update(Guid id, [FromBody] UpdateProductRequest request)
    {
        var product = _productService.UpdateProduct(id, request.Name, request.Description, request.Price, request.Stock);
        return Ok(ProductResponse.FromDomain(product));
    }

    /// <summary>Remove um produto do catálogo.</summary>
    /// <param name="id">GUID do produto.</param>
    /// <response code="204">Produto removido.</response>
    /// <response code="404">Produto não encontrado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        _productService.DeleteProduct(id);
        return NoContent();
    }
}

