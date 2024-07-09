using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Produtos_api.Service.EmployeeIdentity;

namespace Produtos_api.EndPoints.Employees;
class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] {HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    [Authorize]
    //Metodo asincrono para não bloquear tempo de uso na CPU!
    public static async Task<IResult> Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager, EmployeeService employeeService)
    {
         
       var user = new IdentityUser { UserName = employeeRequest.email, Email = employeeRequest.email };
      

        //user ok?
        IEnumerable < IdentityError >? resultUser = await employeeService.SaveEmployee(user, employeeRequest);

        if(resultUser is not null)
        {
                return Results.ValidationProblem(resultUser.convertToProblemsDetails());
        }
        //claim user ok?
        IEnumerable<IdentityError>? resultClaim =
            await employeeService.AddNameAndEmail(user, employeeRequest.employee_code, employeeRequest.name);

        if (resultClaim is not null)
        {
            return Results.ValidationProblem(resultClaim.convertToProblemsDetails());
        }

        return Results.Created($"/employees/{user.Id}",user.Id);
    }
    
}