namespace Application.Utilities.Helpers
{
    internal static class GetError
    {
        public static string Error(string error)
        {
            return $"Error saving {error}.";
        }

        public static string NotFound(Guid id)
        {
            return $"No [{id}] id found.";
        }

        public static string NotFound(string identifier)
        {
            return $"No [{identifier}] found.";
        }
    }
}
