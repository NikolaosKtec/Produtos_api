using Microsoft.AspNetCore.Identity;
using Produtos_api.Service;

namespace Produtos_api.EndPoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(int page,UserManager<IdentityUser> userManager,EmployeeService employeeService)
    {
       if (page < 1)
        {
            return Results.BadRequest("Err! page nao pode ser negativo!");
        }

        List<EmployeeDto> users = employeeService.GetAll_dapper(page);

        return Results.Ok(users);
    }

}
