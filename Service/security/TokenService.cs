using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Produtos_api.Service.security;

public class TokenService
{
    
    public TokenService(string email,string id, IConfiguration configuration) {

        var key = Encoding.ASCII.GetBytes(
            Environment.GetEnvironmentVariable(
            configuration["JwtBearerTokenSettings:Secret_key"]));
         
        TokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, id )
            }),

            SigningCredentials = 
                new SigningCredentials(
                 new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature),
            Audience = configuration["JwtBearerTokenSettings:Audience"],
            Issuer = configuration["JwtBearerTokenSettings:Issuer"],
        };
 
    }

   
    private SecurityTokenDescriptor TokenDescriptor { get; }

    private JwtSecurityTokenHandler _tokenHandler = new();

    public SecurityToken Create_token()
    {
        
        return _tokenHandler.CreateToken(TokenDescriptor);
    }

    public string write_token(SecurityToken token)
    {
       return _tokenHandler.WriteToken(token);
    }


}
