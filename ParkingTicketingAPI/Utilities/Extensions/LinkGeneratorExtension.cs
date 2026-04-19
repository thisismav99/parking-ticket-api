namespace ParkingTicketingAPI.Utilities.Helpers
{
    public static class LinkGeneratorExtension
    {
        public static string? GenerateLink<T>(this LinkGenerator linkGenerator,
            HttpContext httpContext,
            string method,
            string controller,
            T? id)
        {
            string? url = string.Empty;

            if (id is null || id.Equals(default(T)))
            {
                url = linkGenerator.GetUriByAction(httpContext, method, controller);
            }
            else
            {
                url = linkGenerator.GetUriByAction(httpContext, method, controller, new { id = id });
            }

            return url;
        }
    }
}
