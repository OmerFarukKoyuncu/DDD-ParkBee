﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace ParkBee.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class Authorize : Attribute, IAsyncActionFilter
    {
        private const string ApiKey = "x-api-key";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKey, out var apiKeyVal))
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("Api Key not found!");
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(ApiKey);
            if (!apiKey.Equals(apiKeyVal))
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("Unauthorized client");
            }

            await next();
        }
    }
}
