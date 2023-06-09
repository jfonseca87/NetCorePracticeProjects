using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace CookieTest.API.Utils.CustomActionFilters
{
    public class CompareSessionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string sessionId = context.HttpContext.User.Claims.Where(x => x.Type == "SessionId").Select(x => x.Value).FirstOrDefault();
            string cookieSessionId = context.HttpContext.Request.Cookies["SessionId"];

            if (sessionId != cookieSessionId) 
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
