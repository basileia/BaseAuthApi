using BaseAuthApp_BAL.Models;
using BaseAuthApp_BAL.Services;
using BaseAuthApp_DAL.Contracts;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace BaseAuthApp_BAL.Extensions

{
    public class BaseAuthMiddleware : IMiddleware
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly ServiceUser _serviceUser;

        public BaseAuthMiddleware(IRepositoryUser repositoryUser, ServiceUser serviceUser)
        {
            _repositoryUser = repositoryUser;
            _serviceUser = serviceUser;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var bypassPaths = new[] { "/api/Auth/register", "/api/Auth/login" };

            if (bypassPaths.Contains(context.Request.Path.ToString()))
            {
                await next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues value))
            {
                await WriteErrorResponse(context, AuthenticationError.AuthHeaderMissing);
                return;
            }

            var authHeader = value.ToString();
            if (!authHeader.StartsWith("Basic ", System.StringComparison.OrdinalIgnoreCase))
            {
                await WriteErrorResponse(context, AuthenticationError.InvalidAuthHeader);
                return;
            }

            var token = authHeader.Substring("Basic ".Length).Trim();
            var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var credentials = credentialstring.Split(':');
            
            if (credentials != null && credentials.Length == 2)
            {
                var username = credentials[0];
                var password = credentials[1];

                var result = await _serviceUser.ValidateUserWithResultAsync(username, password);
                if (result.IsSuccess)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, username) };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    context.User = new ClaimsPrincipal(identity);
                    await next(context);
                    return;
                }                
            }
            await WriteErrorResponse(context, AuthenticationError.InvalidCredentials);
        }

        private static async Task WriteErrorResponse(HttpContext context, Error error)
        {
            var errorJson = JsonSerializer.Serialize(error);

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(errorJson);
        }
    }
}
