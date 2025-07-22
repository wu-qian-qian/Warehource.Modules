using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Application.Encodings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Common.Infrastructure.Authentication;

public sealed class TokenService(IOptions<JWTOptions> options) : ITokenService
{
    public string BuildJwtString(IEnumerable<Claim> claims)
    {
        var ts = TimeSpan.FromSeconds(options.Value.ExpireSeconds);
        //构建密钥
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));
        //加密算法进行密钥配置
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        //构建安全JWT
        var tokenDescriptor = new JwtSecurityToken(options.Value.Issuer, options.Value.Audience, claims,
            expires: DateTime.Now.Add(ts), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public string BuildJwtString(List<string> roles, List<string> names)
    {
        // 创建Claims
        var claims = new List<Claim>();
        var ts = TimeSpan.FromSeconds(options.Value.ExpireSeconds);


        // 添加角色
        roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

        // 添加权限（自定义声明）
        //     permissions.ForEach(permission => claims.Add(new Claim("Permission", permission)));

        names.ForEach(name => claims.Add(new Claim(ClaimTypes.Name, name)));

        //构建密钥
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        // 创建令牌
        var tokenDescriptor = new JwtSecurityToken(options.Value.Issuer, options.Value.Audience, claims,
            expires: DateTime.Now.Add(ts), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}