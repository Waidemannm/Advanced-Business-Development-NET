using OnlineStore.Domain.Entities;
using Xunit;

namespace OnlineStore.Domain.Tests;

public class CategoryTests
{
    [Fact]
    public void UpdateName_ValidName_UpdatesSuccessfully()
    {
        // Arrange
        var category = new Category("Initial", "Desc");
        var newName = "Valid Name";

        // Act
        category.UpdateName(newName);

        // Assert
        Assert.Equal(newName, category.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void UpdateName_InvalidName_ThrowsArgumentException(string invalidName)
    {
        // Arrange
        var category = new Category("Initial", "Desc");

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => category.UpdateName(invalidName));
        Assert.Equal("Nome inválido.", exception.Message);
    }
}
