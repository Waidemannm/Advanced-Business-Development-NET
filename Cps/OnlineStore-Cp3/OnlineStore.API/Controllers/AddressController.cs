using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTO;
using OnlineStore.Application.Interfaces;

namespace OnlineStore.API.Controllers;

/// <summary>Gerenciamento de endereços dos clientes.</summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService) =>
        _addressService = addressService;

    /// <summary>Retorna todos os endereços cadastrados.</summary>
    /// <response code="200">Lista de endereços retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AddressResponse>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var list = _addressService.GetAll();
        return Ok(list.Select(AddressResponse.FromDomain));
    }

    /// <summary>Busca um endereço pelo identificador único.</summary>
    /// <param name="id">GUID do endereço.</param>
    /// <response code="200">Endereço encontrado.</response>
    /// <response code="404">Endereço não encontrado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AddressResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var address = _addressService.GetById(id);
        if (address is null) return NotFound();
        return Ok(AddressResponse.FromDomain(address));
    }

    /// <summary>Cria um novo endereço.</summary>
    /// <param name="request">Dados do endereço a ser criado.</param>
    /// <response code="201">Endereço criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(AddressResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] CreateAddressRequest request)
    {
        var address = _addressService.CreateAddress(request.ToDomain());
        return CreatedAtAction(nameof(GetById), new { id = address.Id }, AddressResponse.FromDomain(address));
    }

    /// <summary>Atualiza um endereço existente.</summary>
    /// <param name="id">GUID do endereço.</param>
    /// <param name="request">Campos a serem atualizados (parcial).</param>
    /// <response code="200">Endereço atualizado.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Endereço não encontrado.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AddressResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult Update(Guid id, [FromBody] UpdateAddressRequest request)
    {
        var address = _addressService.UpdateAddress(id, request.Street, request.City, request.State, request.PostalCode, request.Number, request.Country);
        return Ok(AddressResponse.FromDomain(address));
    }

    /// <summary>Remove um endereço pelo identificador único.</summary>
    /// <param name="id">GUID do endereço.</param>
    /// <response code="204">Endereço removido.</response>
    /// <response code="404">Endereço não encontrado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        _addressService.DeleteAddress(id);
        return NoContent();
    }
}

