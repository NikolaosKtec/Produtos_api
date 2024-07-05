
using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Produtos_api.Domain.Products;
using Produtos_api.EndPoints.Categorias.dto;
using Produtos_api.Service.Category_service;

namespace Produtos_api.EndPoints.Categorias;

class CategoriaPut
{
    public static string Template => "/categorias/{id:int}";
    public static string[] Methods => new string[] {HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;
    [Authorize]
    static IResult Action([FromRoute]int id,CategoryDto categoriaDto,CategoryService service)
    {
        Category? categoria = service.Get(id);
        

        if (categoria is null)
        {
            return Results.NotFound();
        }
        
        
        // if (categoriaDto.Name.Equals(null) || categoriaDto.Name.Length <=3)
        // {
        //     return Results.BadRequest("parametros sao invalidos!");
        // }
        // else {
            categoria.Set(categoriaDto.Name, categoriaDto.set_activity);
        // }

        

    
        if (!categoria.IsValid)
        {
                return Results.ValidationProblem(categoria.Notifications.convertToProblemsDetails());
        }

        service.Update(categoria);
        return Results.Accepted("ok",(categoriaDto.Name, categoriaDto.set_activity));
    }
}


