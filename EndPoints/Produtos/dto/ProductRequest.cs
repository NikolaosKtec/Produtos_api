using Produtos_api.Domain.Products;

namespace Produtos_api.EndPoints.Produtos;
public record ProductRequest(string Name, int CategoryId, bool HasStock, string Description);