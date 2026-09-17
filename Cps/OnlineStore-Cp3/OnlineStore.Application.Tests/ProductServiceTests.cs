using Moq;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Services;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using Xunit;

namespace OnlineStore.Application.Tests;

public class ProductServiceTests
{
    private readonly Mock<IRepository<Product>> _repositoryMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _repositoryMock = new Mock<IRepository<Product>>();
        _productService = new ProductService(_repositoryMock.Object);
    }

    [Fact]
    public async Task UpdateProductAsync_ProductNotFound_ThrowsResourceNotFoundExceptionAndDoesNotSave()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(nonExistentId))
            .ReturnsAsync((Product?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ResourceNotFoundException>(() => 
            _productService.UpdateProductAsync(nonExistentId, "New Name", "Desc", 10.5m, 10));

        Assert.Contains("Produto", exception.Message);
        
        // Verifica que SaveChangesAsync não foi chamado (caminho de erro)
        _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task CreateProductAsync_ValidProduct_SavesSuccessfully()
    {
        // Arrange
        var product = new Product(Guid.NewGuid(), "Name", "Desc", 10m, 100);

        // Act
        var result = await _productService.CreateProductAsync(product);

        // Assert
        Assert.Equal(product.Id, result.Id);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
