using System.Reflection;

namespace Cramming.Account.Application
{
    public static class ApplicationAssembly
    {
        public static Assembly GetAssembly()
        {
            return typeof(ApplicationAssembly).Assembly;
        }
    }
}
