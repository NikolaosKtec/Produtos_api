using Microsoft.AspNetCore.Authorization;
using Produtos_api.Domain.Products;
using Produtos_api.EndPoints.Categorias.dto;
using Produtos_api.Service.Category_service;

namespace Produtos_api.EndPoints.Categorias;
class CategotiaPost
{
    public static string Template => "/categorias";
    public static string[] Methods => new string[] {HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    [Authorize]
    public static IResult Action(CategoryRequest request, CategoryService service)
    {

        Category categoria = new Category(request.name);
        
        
        
       if(!categoria.IsValid){//todo validação correta
           
           return Results.ValidationProblem(categoria.Notifications.convertToProblemsDetails());
       }

        service.Save(categoria);
        return Results.Created($"/categorias/{request.name}",request.name);
    }
    
}