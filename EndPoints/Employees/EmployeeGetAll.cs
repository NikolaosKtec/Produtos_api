using Microsoft.AspNetCore.Identity;
using Produtos_api.Service;

namespace Produtos_api.EndPoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(UserManager<IdentityUser> userManager,EmployeeService employeeService)
    {

        List<EmployeeDto> users = employeeService.GetAll();

        return Results.Ok(users);
    }

}
