using Produtos_api.EndPoints.Categorias.dto;
using Produtos_api.Service.Category_service;

namespace Produtos_api.EndPoints.Categorias;

class CategoriaGet
{
    public static string Template => "/categorias";
    public static string[] Methods => new string[] {HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(CategoryService categoryService)
    {
        
        IQueryable< CategoryDto >categoria = categoryService.GetAll();
        
       return Results.Ok(categoria);
    }
}