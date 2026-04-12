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
    }
}
