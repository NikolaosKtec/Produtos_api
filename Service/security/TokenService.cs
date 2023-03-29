using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Produtos_api.Service.security;

public class TokenService
{
    public TokenService(string email, IConfiguration configuration) {



        tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email),
            }),

            SigningCredentials = new SigningCredentials(
                 new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        Environment.GetEnvironmentVariable(
                            configuration["JwtBearerTokenSettings:Secret_key"]))

                    ),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Audience = configuration["JwtBearerTokenSettings:Audience"],
            Issuer = configuration["JwtBearerTokenSettings:Issuer"],
        };
 
    }

   
    private SecurityTokenDescriptor tokenDescriptor { get; }

    private JwtSecurityTokenHandler _tokenHandler = new();

    public SecurityToken create_token()
    {
        
        return _tokenHandler.CreateToken(tokenDescriptor);
    }

    public string write_token(SecurityToken token)
    {
       return _tokenHandler.WriteToken(token);
    }


}
