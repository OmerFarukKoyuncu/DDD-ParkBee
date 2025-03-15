using ParkBee.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace parkbee.Application.Helper
{
    public class Helper
    {
        public static void ThrowBusinessRuleException(string applicationMessage, params object[] messageParams)
      => throw new BusinessRuleException(applicationMessage, applicationMessage.Message(messageParams), applicationMessage.UserMessage(messageParams));
    }
}
