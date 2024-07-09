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
    public async Task<IEnumerable<IdentityError>?> SaveEmployee(IdentityUser user, EmployeeRequest employeeRequest)
    {

        var resultUser = await _userManager.CreateAsync(user, employeeRequest.password);


        if (!resultUser.Succeeded)
            return resultUser.Errors;

        return null;
    }

    public async Task<IEnumerable<IdentityError>?> AddNameAndEmail(IdentityUser user, string employee_code, string employee_name)
    {
        var userClaims = new List<Claim>()
        {
            new Claim("employee_code",employee_code),
            new Claim("name_user", employee_name),
        };

        var resultClaim = await _userManager.AddClaimsAsync(user, userClaims);

        if (!resultClaim.Succeeded)
            return resultClaim.Errors;

        return null;
    }

    public async Task<List<EmployeeDto>> GetAll_dapper(int page)
    {
        const int ROWS = 10;
        const string QUERRY = @"
                 SELECT  ""ClaimValue"" AS name , ""Email"",(
	        SELECT
                ""ClaimValue"" as employee_code FROM ""AspNetUserClaims"" c  WHERE
                c.""UserId"" = u.""Id"" AND ""ClaimType"" = 'employee_code') AS employee_code
            FROM ""AspNetUsers"" u 
            INNER JOIN ""AspNetUserClaims"" c ON u.""Id"" = c.""UserId"" 
            AND ""ClaimType"" = 'name_user' 	ORDER BY name
            ";

        var employee =  await _db.QueryAsync<EmployeeDto>(
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
    public async Task<bool> ValidateUserByLogin(UserRequest userRequest)
    {
        var user = await _userManager.FindByEmailAsync(userRequest.email);
        
        if(user is null){
            return false;
        }
        this.id_user = user.Id;
        return  await _userManager.CheckPasswordAsync(user, userRequest.password);
        
    }
}
