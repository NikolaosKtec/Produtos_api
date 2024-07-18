using Produtos_api.Domain.Products;

namespace Produtos_api.EndPoints.Produtos;
public record ProductDto(string? Name, int Id, Category? Category, bool HasStock);