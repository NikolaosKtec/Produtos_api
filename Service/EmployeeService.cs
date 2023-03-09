using Dapper;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using Produtos_api.EndPoints.Employees;
using System.Security.Claims;

namespace Produtos_api.Service;

public class EmployeeService
{
    public EmployeeService(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {

        _db = new NpgsqlConnection(
            Environment.GetEnvironmentVariable(
                configuration.
                GetConnectionString("PostgreSql")));

        _userManager = userManager;
    }
    private readonly NpgsqlConnection _db;


    private readonly UserManager<IdentityUser> _userManager;

    public IEnumerable<IdentityError>? saveEmployee(IdentityUser user, EmployeeRequest employeeRequest)
    {

        var resultUser = _userManager.CreateAsync(user, employeeRequest.password).Result;


        if (!resultUser.Succeeded)
            return resultUser.Errors;

        return null;
    }

    public IEnumerable<IdentityError>? addNameAndEmail(IdentityUser user, string employee_code, string employee_name)
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

    /*public List<EmployeeDto> GetAll(int page) // depreciado
    {
        const int ROWS = 10;

        var identityUsers = _userManager.Users.Skip((page-1)* ROWS).Take(ROWS).ToList();
        List <EmployeeDto> users = new List<EmployeeDto>();

        identityUsers.ForEach((user) =>
        {
            var claims = _userManager.GetClaimsAsync(user).Result;

            Claim? userNameClaim = claims.FirstOrDefault(c => c.Type == "name_user");
            string userName = userNameClaim is null? string.Empty: userNameClaim.Value;

            Claim? codeClaim = claims.FirstOrDefault(c => c.Type == "employee_code");
            string code = codeClaim is null ? string.Empty : codeClaim.Value;

            var dto = new EmployeeDto(userName, user.Email, code);
            users.Add(dto);
        });

        return users;
    }*/

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
}
