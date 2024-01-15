using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(CustomUser user, List<string> roles);
    }
}
