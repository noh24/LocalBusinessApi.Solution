using LocalBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LocalBusiness.Repository;

public class JWTManagerRepository : IJWTManagerRepository
{
  Dictionary<string, string> UsersRecords = new Dictionary<string, string>
  {
    { "user1","password1"},
    { "user2","password2"},
    { "user3","password3"},
  };

  private readonly IConfiguration iconfiguration;
  public JWTManagerRepository(IConfiguration iconfiguration)
  {
    this.iconfiguration = iconfiguration;
  }
  public Tokens Authenticate(Users users)
  {
    if (!UsersRecords.Any(x => x.Key == users.Name && x.Value == users.Password))
    {
      return null;
    }

    // Else we generate JSON Web Token
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new Claim[]
      {
       new Claim(ClaimTypes.Name, users.Name)
      }),
      Expires = DateTime.UtcNow.AddHours(10),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return new Tokens { Token = tokenHandler.WriteToken(token) };

  }
}