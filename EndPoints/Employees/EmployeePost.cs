using Microsoft.AspNetCore.Identity;
using Produtos_api.Service;


namespace Produtos_api.EndPoints.Employees;
class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] {HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager, EmployeeService employeeService)
    {

       var user = new IdentityUser { UserName = employeeRequest.email, Email = employeeRequest.email };
      

        //user ok?
        IEnumerable < IdentityError >? resultUser = employeeService.saveEmployee(user, employeeRequest);

        if(!(resultUser is null))
        {
                return Results.ValidationProblem(resultUser.convertToProblemsDetails());
        }
        //claim user ok?
        IEnumerable<IdentityError>? resultClaim =
            employeeService.addNameAndEmail(user, employeeRequest.employee_code, employeeRequest.name);

        if (!(resultClaim is null))
        {
            return Results.ValidationProblem(resultClaim.convertToProblemsDetails());
        }

        return Results.Created($"/employees/{user.Id}",user.Id);
    }
    
}