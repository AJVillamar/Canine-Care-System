using System.Globalization;

namespace Domain.Shared.Utils
{
    public static class StringUtils
    {
        public static string Capitalize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            TextInfo text = CultureInfo.CurrentCulture.TextInfo;
            return text.ToTitleCase(value.ToLower().Trim());
        }
    }
}
