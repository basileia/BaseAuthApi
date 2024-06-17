using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAuthApp_BAL.Extensions
{
    public class BaseAuthMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authorization header missing.");
                return;
            }

            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (!authHeader.StartsWith("Basic ", System.StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid authorization header.");
                return;
            }

            var token = authHeader.Substring("Basic ".Length).Trim();
            var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var credentials = credentialstring.Split(':');


        }
    }
}
