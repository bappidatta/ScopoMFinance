using System;
using System.Web;

namespace ScopoMFinance.Core.Helpers
{
    /// <summary>
    /// Class that accesses the cookie collection from current request HttpContext
    /// </summary>
    public interface ICookieAccessor
    {
        /// <summary>
        /// Returns cookie collection from HttpContext.Current.Request.Cookies
        /// </summary>
        HttpCookieCollection requestCookies { get; }

        /// <summary>
        /// Returns cookie collection from HttpContext.Current.Reponse.Cookies
        /// </summary>
        HttpCookieCollection responseCookies { get; }

        /// <summary>
        /// Remove a cookie from the HttpContext.Current.Response.Cookies collection
        /// </summary>
        /// <param name="cookieName"> name of the cookie to remove </param>
        void ClearCookie(string cookieName);

        /// <summary>
        /// Updates the value of an existing cookie or creates a new one
        /// </summary>
        /// <param name="cookieName"> name of the cookie to update </param>
        /// <param name="value"> value to store in the cookie </param>
        void UpdateCookie(string cookieName, object value);
    }

    /// <summary>
    /// Class that accesses the cookie collection from current request HttpContext
    /// </summary>
    public class CookieAccessor : ICookieAccessor
    {
        /// <summary>
        /// Returns cookie collection from HttpContext.Current.Request.Cookies
        /// </summary>
        public HttpCookieCollection requestCookies { get { return HttpContext.Current.Request.Cookies; } }

        /// <summary>
        /// Returns cookie collection from HttpContext.Current.Reponse.Cookies
        /// </summary>
        public HttpCookieCollection responseCookies { get { return HttpContext.Current.Response.Cookies; } }

        /// <summary>
        /// Remove a cookie from the HttpContext.Current.Response.Cookies collection
        /// </summary>
        /// <param name="cookieName"> name of the cookie to remove </param>
        public void ClearCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Response.Cookies[cookieName];

            if (cookie != null)
            {
                // set cookie as expired
                cookie.Expires = DateTime.Now.AddDays(-1);

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// Updates the value of an existing cookie or creates a new one
        /// </summary>
        /// <param name="cookieName"> name of the cookie to update </param>
        /// <param name="value"> value to store in the cookie </param>
        public void UpdateCookie(string cookieName, object value)
        {
            HttpCookie cookie = HttpContext.Current.Response.Cookies[cookieName] ?? new HttpCookie(cookieName);
            cookie.Value = value.ToString();

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
