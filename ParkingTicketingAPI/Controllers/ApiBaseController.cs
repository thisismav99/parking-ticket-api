using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ParkingTicketingAPI.Utilities.Enums;
using ParkingTicketingAPI.Utilities.Helpers;

namespace ParkingTicketingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public abstract class ApiBaseController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        protected ApiBaseController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        protected IActionResult CreatedAtPostResponse<T>(IEnumerable<LinkKeys> keys,
            T? value)
        {
            var linkGenerator = new Dictionary<string, string?>();

            foreach (var key in keys)
            {
                switch (key)
                {
                    case LinkKeys.ById:
                        linkGenerator.Add("ById", _linkGenerator.GenerateLink(HttpContext, "Get", ControllerContext.ActionDescriptor.ControllerName, value));
                        break;
                    case LinkKeys.List:
                        linkGenerator.Add("List", _linkGenerator.GenerateLink<string>(HttpContext, "Get", ControllerContext.ActionDescriptor.ControllerName, null));
                        break;
                    case LinkKeys.UpdateById:
                        linkGenerator.Add("UpdateById", _linkGenerator.GenerateLink(HttpContext, "Update", ControllerContext.ActionDescriptor.ControllerName, value));
                        break;
                    case LinkKeys.DeleteById:
                        linkGenerator.Add("DeleteById", _linkGenerator.GenerateLink(HttpContext, "Delete", ControllerContext.ActionDescriptor.ControllerName, value));
                        break;
                    case LinkKeys.AddRoleClaim:
                        linkGenerator.Add("AddRoleClaim", _linkGenerator.GenerateLink<string>(HttpContext, "Claim", ControllerContext.ActionDescriptor.ControllerName, null));
                        break;
                    case LinkKeys.AddRole:
                        linkGenerator.Add("AddRole", _linkGenerator.GenerateLink<string>(HttpContext, "Post", ControllerContext.ActionDescriptor.ControllerName, null));
                        break;
                    case LinkKeys.AddUserRole:
                        linkGenerator.Add("AddUserRole", _linkGenerator.GenerateLink<string>(HttpContext, "AddUserRole", ControllerContext.ActionDescriptor.ControllerName, null));
                        break;
                    case LinkKeys.AddUserClaim:
                        linkGenerator.Add("AddUserClaim", _linkGenerator.GenerateLink<string>(HttpContext, "AddUserClaim", ControllerContext.ActionDescriptor.ControllerName, null));
                        break;
                    case LinkKeys.Self:
                        linkGenerator.Add("Self", _linkGenerator.GenerateLink<string>(HttpContext, "Post", ControllerContext.ActionDescriptor.ControllerName, null));
                        break;
                    default:
                        break;
                }
            }

            return Created(linkGenerator["ById"], linkGenerator);
        }
    }
}
