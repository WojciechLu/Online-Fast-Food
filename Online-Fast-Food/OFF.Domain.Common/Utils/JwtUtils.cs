using Microsoft.IdentityModel.Tokens;
using OFF.Domain.Common.Models.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Utils;

public interface IJwtUtils
{
    string GenerateJWT(UserAuthorizeDTO user);
    int? ValidateToken(string? token);
}

public class JwtUtils : IJwtUtils
{
    private readonly AuthenticationSettings _authenticationSettings;

    public JwtUtils(AuthenticationSettings authenticationSettings)
    {
        _authenticationSettings = authenticationSettings;
    }

    public string GenerateJWT(UserAuthorizeDTO user)
    {
        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims, expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public int? ValidateToken(string? token)
    {
        if (token == null)
            return null;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authenticationSettings.JwtKey);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            return userId;
        }
        catch
        {
            return null;
        }
    }
}