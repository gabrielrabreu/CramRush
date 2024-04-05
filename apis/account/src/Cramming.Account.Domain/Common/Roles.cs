namespace Cramming.Account.Domain.Common
{
    public static class Roles
    {
        public const string Administrator = nameof(Administrator);
        public const string User = nameof(User);

        public static List<string> GetAllRoles()
        {
            return typeof(Roles)
                .GetFields()
                .Where(f => f.IsLiteral)
                .Select(f => f.GetValue(null)!.ToString()!)!
                .ToList();
        }
    }
}
