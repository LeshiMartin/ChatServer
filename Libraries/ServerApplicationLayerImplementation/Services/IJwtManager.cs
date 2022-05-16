using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerApplicationLayerImplementation.Services;
internal interface IJwtManager
{
  string CreateToken ( string username );
  string? GetUsernameFromToken ( string token );
}

internal sealed class JwtManager : IJwtManager
{
  private readonly IConfiguration _configuration;

  public JwtManager ( IConfiguration configuration )
  {
    _configuration = configuration;
  }
  public string CreateToken ( string username )
  {
    var tokenHandler = new JwtSecurityTokenHandler ();
    var key = Encoding.ASCII.GetBytes (_configuration[ "Jwt:Key" ]);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity (new Claim[]
      {
        new Claim (ClaimTypes.Name, username)
      }),
      Expires = DateTime.UtcNow.AddDays (7),
      SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken (tokenDescriptor);
    return tokenHandler.WriteToken (token);
  }
  public string? GetUsernameFromToken ( string token )
  {
    var tokenHandler = new JwtSecurityTokenHandler ();
    var key = Encoding.ASCII.GetBytes (_configuration[ "Jwt:Key" ]);
    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey (key),
      ValidateIssuer = false,
      ValidateAudience = false
    };
    var claims = tokenHandler.ValidateToken (token, tokenValidationParameters, out var securityToken);
    return claims.FindFirst (ClaimTypes.Name)?.Value;
  }
}
