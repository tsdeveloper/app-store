namespace Helpers.Extension.String
{
    public static class StringExtension
    {
        public static string ConvertNameConventionUrlWeb(this string text)
        {
            
            if (string.IsNullOrEmpty(text))
                return text;

            var textConvention = text.Replace(" ", "-")
                .Replace(".", "-")
                .Replace(",", "-")
                .Replace("_", "-");

            return textConvention;

        }
        
    }
}