using Microsoft.AspNetCore.Identity;
using Produtos_api.EndPoints;
using Produtos_api.EndPoints.Employees;
using System.Security.Claims;

namespace Produtos_api.Service;

 public class EmployeeService
{
    public EmployeeService(UserManager<IdentityUser> userManager) => _userManager= userManager;


    private readonly UserManager<IdentityUser> _userManager;

    public IEnumerable<IdentityError>? saveEmployee(IdentityUser user, EmployeeRequest employeeRequest)
    {
       
        var resultUser = _userManager.CreateAsync(user, employeeRequest.password).Result;
        

        if (!resultUser.Succeeded)
            return resultUser.Errors;

        return null;
    }

    public IEnumerable<IdentityError>? addNameAndEmail(IdentityUser user,string employee_code, string employee_name)
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

    public List<EmployeeDto> GetAll() 
    {
        var identityUsers = _userManager.Users.ToList();
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
    } 
}
