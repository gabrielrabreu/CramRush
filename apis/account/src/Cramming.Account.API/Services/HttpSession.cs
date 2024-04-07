using Cramming.Account.Application.Common.Interfaces;
using System.Security.Claims;

namespace Cramming.Account.API.Services
{
    public class HttpSession(IHttpContextAccessor httpContextAccessor) : IHttpSession
    {
        public string? UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? UserName => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
    }
}
