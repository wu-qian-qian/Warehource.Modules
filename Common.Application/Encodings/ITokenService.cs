using System.Security.Claims;

namespace Common.Application.Encodings;

public interface ITokenService
{
    string BuildJwtString(IEnumerable<Claim> claims);
    string BuildJwtString(List<string> roles, List<string> permissions);
}