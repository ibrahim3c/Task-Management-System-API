using Twilio.Rest.Api.V2010.Account;

namespace Task_Management_System_API.Services.Interfaces
{
    public interface ISMSService
    {
        MessageResource SendSMS(string PhoneNumber, string Body);
    }
}
