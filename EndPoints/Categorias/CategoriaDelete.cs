
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Produtos_api.Domain.Products;
using Produtos_api.Service.Category_service;

namespace Produtos_api.EndPoints.Categorias;
class CategoriaDelete
{
    public static string Template => "/categorias/{id:int}";
    public static string[] Methods => new string[] {HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;
   
    public static async Task<IResult> Action([FromRoute]int id,CategoryService service)
    {
        
        // Category? categoria =  await service.GetAsync(id);

        // if(categoria is null){
        //     return Results.NoContent();
        // }
        // // gatilho pode ser útil aqui

        // categoria.set_invalidate();
        

        // if (!is_ok) {
        //     return Results.Problem("Tente novamente! ,essa categoria não foi foi desabilitada!");
        // }
        
       // service.inactivate_childs(categoria);// todo aparenemente feito
        return Results.Ok(new{satus="Este agora encontra-se Inativo!"});
    }
}