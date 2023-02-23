using Produtos_api.Domain.Products;
using Produtos_api.Service;

namespace Produtos_api.EndPoints.Categorias;
class CategotiaPost
{
    public static string Template => "/categorias";
    public static string[] Methods => new string[] {HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(CategoryDto categoriaDto, CategoryService service)
    {

        Category categoria = new Category(categoriaDto.Name);
        
        
        
       if(!categoria.IsValid){//todo validação correta
           
           return Results.ValidationProblem(categoria.Notifications.convertToProblemsDetails());
       }

        service.Save(categoria);
        return Results.Created($"/categorias/{categoriaDto.Id}",categoriaDto.Name);
    }
    
}