using Microsoft.IdentityModel.Tokens;
using Produtos_api.Service.EmployeeIdentity;
using Produtos_api.Service.security;

namespace Produtos_api.EndPoints.Authentication;

public class Login
{
    public static string Template => "/login";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;


    public static IResult Action(UserDto userDto,EmployeeService employeeService )
    {
       
        var user = employeeService.find_user_by_email(userDto.Email);

        if(user is null)
            return Results.BadRequest();

        if(! employeeService.check_password(user,userDto.Password))
            return Results.BadRequest();

        //if user exist and password ok
        var token_provider = new TokenService(userDto.Email);

        SecurityToken token = token_provider.create_token();

        return Results.Ok(token_provider.write_token(token));
    }


}
