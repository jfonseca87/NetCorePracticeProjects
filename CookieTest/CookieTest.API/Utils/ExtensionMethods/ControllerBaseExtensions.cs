using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookieTest.API.Utils.ExtensionMethods
{
    public static class ControllerBaseExtensions
    {
        public static void SetCookie(this ControllerBase controller, string key, string value)
        {
            CookieOptions options = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            };
            
            controller.Response.Cookies.Append(key, value, options);
        }
    }
}
