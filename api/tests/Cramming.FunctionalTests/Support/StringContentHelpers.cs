using System.Text;
using System.Text.Json;

namespace Cramming.FunctionalTests.Support
{
    public static class StringContentHelpers
    {
        public static StringContent FromModelAsJson(this object model)
        {
            return new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        }
    }
}
