using System.Security.Claims;

namespace API.Contracts
{
    public interface ITokenHandler
    {
        string GenerateToken(ICollection<Claim> claims);
    }
}
