using System.Globalization;
using System.Text;

namespace Cramming.SharedKernel
{
    public static class StringExtensions
    {
        public static string RemoveSpacesAndAccents(this string text)
        {
            var sb = new StringBuilder();
            foreach (char c in text)
            {
                if (!char.IsWhiteSpace(c))
                    sb.Append(c);
            }

            string normalizedText = sb.ToString().Normalize(NormalizationForm.FormD);
            var result = new StringBuilder();
            foreach (char c in normalizedText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    result.Append(c);
            }

            return result.ToString();
        }
    }
}
