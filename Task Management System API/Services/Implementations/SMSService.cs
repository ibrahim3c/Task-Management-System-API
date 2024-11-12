using Microsoft.Extensions.Options;
using Task_Management_System_API.Helpers;
using Task_Management_System_API.Services.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Task_Management_System_API.Services.Implementations
{
    public class SMSService:ISMSService
    {
        private readonly IOptionsMonitor<TwilioConfigs> twilioConfigs;

        public SMSService(IOptionsMonitor<TwilioConfigs> TwilioConfigs)
        {
            twilioConfigs = TwilioConfigs;
        }
        public MessageResource SendSMS(string PhoneNumber,string Body)
        {
            // inialize
            TwilioClient.Init(twilioConfigs.CurrentValue.AccountSID, twilioConfigs.CurrentValue.AuthToken);

            // create 
            var result =  MessageResource.Create(
                body: Body,
                from: new Twilio.Types.PhoneNumber(twilioConfigs.CurrentValue.PhoneNumber),
                to: PhoneNumber
                );

            return result;
        }
    }
}
