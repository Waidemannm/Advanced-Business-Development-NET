using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Infrastructure.Persistence;

namespace OnlineStore.Infrastructure.Services;

public class CategoryService(OnlineStoreContext context) : ICategoryService
{
    public Category CreateCategory(Category category)
    {
        context.Categories.Add(category);
        context.SaveChanges();
        return category;
    }

    public Category? GetById(Guid id) =>
        context.Categories.FirstOrDefault(c => c.Id == id);

    public Category? GetCategoryByName(string name) =>
        context.Categories.FirstOrDefault(c => c.Name == name);

    public IReadOnlyList<Category> GetAll() =>
        context.Categories.ToList();

    public Category UpdateCategory(Guid id, string? name, string? description)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id)
            ?? throw new ResourceNotFoundException("Categoria", id);

        if (name is not null) category.UpdateName(name);
        if (description is not null) category.UpdateDescription(description);

        context.SaveChanges();
        return category;
    }

    public void DeleteCategory(Guid id)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id)
            ?? throw new ResourceNotFoundException("Categoria", id);
        context.Categories.Remove(category);
        context.SaveChanges();
    }
}
