using Microsoft.AspNetCore.Authorization;
using Produtos_api.Service.Produtos_service;

namespace Produtos_api.EndPoints.Produtos;
class ProdutosGet
{
    public static string Template = "/produtos";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(ProdutosService service)
    {   
        IQueryable<ProductDto> productDtos = service.GetAll();

        return Results.Ok(productDtos);
    }
}