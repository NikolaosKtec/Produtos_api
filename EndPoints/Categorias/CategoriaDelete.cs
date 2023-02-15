
using Microsoft.AspNetCore.Mvc;
using Produtos_api.Domain.Products;
using Produtos_api.Service;

namespace Produtos_api.EndPoints.Categorias;
class CategoriaDelete
{
    public static string Template => "/categorias/{id:int}";
    public static string[] Methods => new string[] {HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action([FromRoute]int id,CategoryService service)
    {
        
        Category categoria =  service.Get(id);

        if(categoria is null){
            return Results.NoContent();
        }
        //todo gatilho pode ser útil aqui
        //categoria.set_active(false);
        service.Update(categoria);
        
        service.inactivate_childs(categoria);// todo aparenemente feito
        return Results.Ok("Este agora encontra-se Inativo!");
    }
}