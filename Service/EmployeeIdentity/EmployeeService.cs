using Dapper;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using Produtos_api.EndPoints.Authentication;
using Produtos_api.EndPoints.Employees;
using System.Security.Claims;

namespace Produtos_api.Service.EmployeeIdentity;

public class EmployeeService
{
    public EmployeeService(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {

        _db = new NpgsqlConnection(
            Environment.GetEnvironmentVariable(
                configuration["ConnectionStrings:PostgreSql"]));

        _userManager = userManager;
    }
    private readonly NpgsqlConnection _db;
    private readonly UserManager<IdentityUser> _userManager;
    private string id_user = "";
    public IEnumerable<IdentityError>? SaveEmployee(IdentityUser user, EmployeeRequest employeeRequest)
    {

        var resultUser = _userManager.CreateAsync(user, employeeRequest.password).Result;


        if (!resultUser.Succeeded)
            return resultUser.Errors;

        return null;
    }

    public IEnumerable<IdentityError>? AddNameAndEmail(IdentityUser user, string employee_code, string employee_name)
    {
        var userClaims = new List<Claim>()
        {
            new Claim("employee_code",employee_code),
            new Claim("name_user", employee_name),
        };

        var resultClaim = _userManager.AddClaimsAsync(user, userClaims).Result;

        if (!resultClaim.Succeeded)
            return resultClaim.Errors;

        return null;
    }

    public List<EmployeeDto> GetAll_dapper(int page)
    {
        const int ROWS = 10;
        const string QUERRY = @"
                SELECT  ""ClaimValue"" as name, ""Email"" as email 
                from ""AspNetUsers"" u inner join ""AspNetUserClaims"" c 
                on u.""Id"" = c.""UserId"" and ""ClaimType"" = 'name_user'
                ORDER BY name
                OFFSET (@page -1)*@ROWS FETCH NEXT @ROWS ROWS ONLY
                ";

        var employee = _db.Query<EmployeeDto>(
            QUERRY,
            new { page, ROWS }
        );
        return employee.ToList();
    }
    // Use somente depois de o usuário for autenticado!!!
    public string ShowIdCurrentUser(){
        return id_user;
    }
    //Esse método verifica se o usuário existe, e o valida as credencias.
    // Caso não exista retornará falso
    public bool ValidateUserByLogin(UserRequest userRequest)
    {
        var user = _userManager.FindByEmailAsync(userRequest.email).Result;
        
        if(user is null){
            return false;
        }
        this.id_user = user.Id;
        return  _userManager.CheckPasswordAsync(user, userRequest.password).Result;
        
    }
}
