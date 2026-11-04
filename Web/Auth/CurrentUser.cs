using System.Security.Claims;
using Application.Contracts;

namespace Web.Auth;

public class CurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    public Guid GetUserId()
    {
        var id = accessor.HttpContext?.User.FindFirst(c => c.Type == ClaimTypes.Name);
        return Guid.Parse(id != null ? id.Value : "");
    }
}