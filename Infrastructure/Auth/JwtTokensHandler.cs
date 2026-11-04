using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JwtTokensHandler : IAuthTokensHandler
{
    private List<Claim> _claims = [];
    
    public Dictionary<string, string> CreateTokens(Guid userId, out string jti)
    {
        jti = Guid.NewGuid().ToString();
        List<Claim> rClaims = [new(ClaimTypes.Name, userId.ToString()), new (ClaimTypes.Authentication, jti) ];
        List<Claim> aClaims = [new(ClaimTypes.Name, userId.ToString())];

        JwtSecurityToken rToken = new(issuer: "issuer", audience: "audience", claims: rClaims, expires: DateTime.UtcNow.AddDays(30), signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dksbvdsbvdisavbdosavdsbhvjlsdbvadbvlajdsbvdvbdsaivudavb")), SecurityAlgorithms.HmacSha256));
        JwtSecurityToken aToken = new(issuer: "issuer", audience: "audience", claims: aClaims, expires: DateTime.UtcNow.AddMinutes(15), signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dksbvdsbvdisavbdosavdsbhvjlsdbvadbvlajdsbvdvbdsaivudavb")), SecurityAlgorithms.HmacSha256));
        
        string strRToken = new JwtSecurityTokenHandler().WriteToken(rToken);
        string strAToken = new JwtSecurityTokenHandler().WriteToken(aToken);

        Dictionary<string, string> tokens = [];
        tokens.Add("accessToken", strAToken);
        tokens.Add("refreshToken", strRToken);
        return tokens;
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        var result = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "issuer",
            ValidateAudience = true,
            ValidAudience = "audience",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dksbvdsbvdisavbdosavdsbhvjlsdbvadbvlajdsbvdvbdsaivudavb"))
        });
        if (result.IsValid)
            _claims = result.ClaimsIdentity.Claims.ToList();
        return result.IsValid;
    }

    public string? GetClaim(string claimType)
    {
        string? claimValue = _claims.FirstOrDefault(claim => claim.Type.EndsWith($"/{claimType.ToLower()}"))?.Value;
        if(string.IsNullOrEmpty(claimValue))
            return null;
        return claimValue;
    }
}