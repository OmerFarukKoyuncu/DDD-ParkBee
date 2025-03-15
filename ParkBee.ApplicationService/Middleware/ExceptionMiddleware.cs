using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using parkbee.Application.Helper;
using ParkBee.Domain.Constants;

namespace parkbee.Application.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        private static JsonSerializerSettings SerializeSettings => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            string messageCode;
            string message;
            string userMessage;

            if (ex is BusinessRuleException businessRuleException)
            {
                messageCode = businessRuleException.Code;
                message = businessRuleException.Message;
                userMessage = businessRuleException.UserMessage;
            }
            else
            {
                var appMsg = ApplicationMessage.Failed;
                messageCode = appMsg;
                message = appMsg.Message();
                userMessage = appMsg.UserMessage();
            }

            var res = new ResponseBase<object>
            {
                Success = false,
                MessageCode = messageCode,
                Message = message,
                UserMessage = userMessage
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(res, SerializeSettings));
        }
    }
}
