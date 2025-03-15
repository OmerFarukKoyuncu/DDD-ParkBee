using ParkBee.Domain.Constants;
using System.Net.NetworkInformation;

namespace ParkBee.Application.Contracts.Implementations
{
    public class PingService : IPingService
    {
        public async Task<ResponseBase> CheckGarageDoor(string doorIpAddress)
        { 
            var pingResponse = new ResponseBase();
            var ping = new Ping();

            try
            {
                var response = ping.Send(doorIpAddress, 1000);

                if (response.Status != null && response.Status.ToString().Equals("Success"))
                {
                    pingResponse.Success = true;
                    pingResponse.Message = ApplicationMessage.UserMessage(ApplicationMessage.DoorIsReachable);

                    return pingResponse;
                }

            }
            catch (Exception ex)
            {
                pingResponse.Success = false;
                pingResponse.Message = ex.Message;
            }

            return pingResponse;
        }
    }
}
