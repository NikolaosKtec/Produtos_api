using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Produtos_api.Domain.Products;
using Produtos_api.EndPoints.Categorias.dto;
using Produtos_api.Service.Category_service;

namespace Produtos_api.EndPoints.Categorias;
class CategoriaPost
{
    public static string Template => "/categorias";
    public static string[] Methods => new string[] {HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(List<CategoryRequest> request, CategoryService service)
    {

        List<Category> categoria = new List<Category>();
        var problemsDetails = new Dictionary<string, string[]>();
        //cria uma nova categoria, e adiciona na lista categoria
        request.ForEach(r => {
            categoria.Add(new Category(r.name));
        });

        // validação correta
        categoria.ForEach(c => {

            if(!c.IsValid)
                problemsDetails = c.Notifications.convertToProblemsDetails();
                
                
        });
       
       if(!(problemsDetails.Count == 0))
            return Results.ValidationProblem(problemsDetails);

        await service.SaveAsync(categoria);
        return Results.Created("/categorias",new{created= categoria.Count});
    }
    
}