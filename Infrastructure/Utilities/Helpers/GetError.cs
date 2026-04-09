namespace Infrastructure.Utilities.Helpers
{
    internal static class GetError
    {
        public static string Error(string code, string description)
        {
            if (string.IsNullOrEmpty(code))
            {
                return $"Error: {description}";
            }
            else
            {
                return $"Error Code: {code} - {description}";
            }
        }
    }
}
