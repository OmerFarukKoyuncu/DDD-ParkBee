using System;
using System.Net;

namespace parkbee.Application.Helper
{
    public class BusinessRuleException : Exception
    {
        public string Code { get; set; }
        public new string Message { get; set; }
        public string UserMessage { get; set; }

        protected BusinessRuleException()
        {
        }

        public BusinessRuleException(
            string code,
            string message,
            string userMessage)
            : base(message)
        {
            Code = code;
            Message = message;
            UserMessage = userMessage;
        }

        protected BusinessRuleException(string message)
            : base(message)
        {
        }
    }
}