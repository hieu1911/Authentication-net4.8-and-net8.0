using Microsoft.AspNetCore.Authentication;
using Synercoding.FormsAuthentication;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LoginAndChatRealTime.Helper
{
    public static class FormsAuthHelper
    {
        public static AuthenticationTicket ConvertCookieToTicket(FormsAuthenticationCookie cookie)
        {
            var authenticationProperties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                ExpiresUtc = cookie.ExpiresUtc,
                IsPersistent = cookie.IsPersistent,
                IssuedUtc = cookie.IssuedUtc
            };

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, cookie.UserName));
            identity.AddClaim(new Claim("Id", cookie.UserData));

            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationTicket(principal, authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public static FormsAuthenticationCookie ConvertTicketToCookie(AuthenticationTicket ticket)
        {
            var claimsIdentity = (ClaimsIdentity)ticket.Principal.Identity;

            var cookie = new FormsAuthenticationCookie()
            {
                CookiePath = "",
                ExpiresUtc = (ticket.Properties.ExpiresUtc ?? DateTime.Now).DateTime.ToUniversalTime(),
                IsPersistent = ticket.Properties.IsPersistent,
                IssuedUtc = (ticket.Properties.IssuedUtc ?? DateTime.Now).DateTime.ToUniversalTime(),
                UserName = claimsIdentity.Name,
                UserData = claimsIdentity.FindFirst("Id").Value
            };

            return cookie;
        }
    }
}
