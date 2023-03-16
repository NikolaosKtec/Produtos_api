using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Produtos_api.Service.security;

public class TokenService
{
    public TokenService(string email) {


        tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email),
            }),

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_key),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Audience = "Produtos_api",
            Issuer = "Issuer",
        };


    }

    private SecurityTokenDescriptor tokenDescriptor { get; }

    private byte[] _key = Encoding.ASCII.GetBytes("Fa65GA0@564BtgTe.");

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
