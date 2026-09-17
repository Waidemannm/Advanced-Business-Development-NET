using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces;

public interface ICategoryService
{
    Category CreateCategory(Category category);
    Category? GetById(Guid id);
    Category? GetCategoryByName(string name);
    IReadOnlyList<Category> GetAll();
    Category UpdateCategory(Guid id, string? name, string? description);
    void DeleteCategory(Guid id);
}
