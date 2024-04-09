using Cramming.Application.Common.Interfaces;
using System.Security.Claims;

namespace Cramming.API.Scope.Services
{
    public class HttpSession(IHttpContextAccessor httpContextAccessor) : IHttpSession
    {
        public string? UserName => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
    }
}
