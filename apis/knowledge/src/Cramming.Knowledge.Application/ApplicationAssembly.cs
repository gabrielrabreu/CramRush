using System.Reflection;

namespace Cramming.Knowledge.Application
{
    public static class ApplicationAssembly
    {
        public static Assembly GetAssembly()
        {
            return typeof(ApplicationAssembly).Assembly;
        }
    }
}
