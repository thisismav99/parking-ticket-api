namespace ParkingTicketingAPI.Utilities.Helpers
{
    public static class LinkGeneratorExtension
    {
        public static string? GenerateLink(this LinkGenerator linkGenerator,
            HttpContext httpContext,
            string method,
            string controller,
            Guid? id)
        {
            string? url = string.Empty;

            if (id is null || id == Guid.Empty)
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
