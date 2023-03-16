
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Produtos_api.Domain.Products;
using Produtos_api.Service.Category;

namespace Produtos_api.EndPoints.Categorias;

class CategoriaPut
{
    public static string Template => "/categorias/{id:int}";
    public static string[] Methods => new string[] {HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;
    static IResult Action([FromRoute]int id,CategoryDto categoriaDto,CategoryService service)
    {
        CategoryDomain categoria = service.Get(id);
        

        if (categoria is null)
        {
            return Results.NotFound();
        }
        else
        {
            if (categoriaDto.Name.Length < 1)
            {
                categoria.define(categoriaDto.set_activity);
            }
            else {
                categoria.define(categoriaDto.Name, categoriaDto.set_activity);
            }

        }

    
        if (!categoria.IsValid)
        {
                return Results.ValidationProblem(categoria.Notifications.convertToProblemsDetails());
        }

        service.Update(categoria);
        return Results.Accepted("ok",(categoriaDto.Name, categoriaDto.set_activity));
    }
}


