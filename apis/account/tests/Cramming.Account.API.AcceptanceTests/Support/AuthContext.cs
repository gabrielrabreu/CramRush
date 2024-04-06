using System.Security.Claims;

namespace Cramming.Account.API.AcceptanceTests.Support
{
    public class AuthContext(BearerContext bearerContext)
    {
        public string? LoggedInUserName { get; set; }
        public string? LoggedInUserId { get; set; }
        public string? AccessToken { get; set; }

        public void AsDefaultUser()
        {
            AsUser("test@local", []);
        }

        public void AsScopedUser(params string[] scopes)
        {
            AsUser("test@local", scopes);
        }

        private void AsUser(string userName, string[] scopes)
        {
            var userId = Guid.NewGuid().ToString();

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Name, userName)
            };

            foreach (var scope in scopes)
                claims.Add(new("Scope", scope));

            LoggedInUserId = userId;
            LoggedInUserName = userName;
            AccessToken = bearerContext.GenerateToken(claims);
        }
    }
}
