using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Produtos_api.Service.EmployeeIdentity;
using Produtos_api.Service.security;

namespace Produtos_api.EndPoints.Authentication;

public class Login
{
    public static string Template => "/login";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

[AllowAnonymous]
    public static IResult Action(UserRequest userRequest,EmployeeService employeeService, IConfiguration configuration)
    {
       
        if(!employeeService.ValidateUserByLogin(userRequest))
            return Results.BadRequest( "email ou senha incoretos!");
        
        
        // if user exist and password ok
        var token_provider = new TokenService(userRequest.email,
        employeeService.ShowIdCurrentUser(),
        configuration);

        SecurityToken token = token_provider.Create_token();

        return Results.Ok(token_provider.write_token(token));
    }


}
