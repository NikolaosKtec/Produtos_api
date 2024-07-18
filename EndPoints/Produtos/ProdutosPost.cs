using Produtos_api.Domain.Products;
using Produtos_api.Service.Produtos_service;

namespace Produtos_api.EndPoints.Produtos;
class ProdutosPost
{
    public static string Template = "/produtos";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(ProductRequest request, ProdutosService service)
    {
        

        Product product = new Product(request.Name, request.Description, request.CategoryId, request.HasStock);
        
        
        
       if(!product.IsValid){//todo validação correta
           
           return Results.ValidationProblem(product.Notifications.convertToProblemsDetails());
       }
        await service.SaveAsync(product);
        return Results.Created($"/categorias/{request.Name}",request.Name);
    }
}