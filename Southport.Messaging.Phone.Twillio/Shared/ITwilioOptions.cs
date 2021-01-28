namespace Southport.Messaging.Phone.Twilio.Shared
{
    public interface ITwilioOptions
    {
        string AccountSid { get; set; }
        string ApiKey { get; set; }
        string AuthToken { get; set; }
        bool UseSandbox { get; set; }
        string TestPhoneNumbers { get; set; }
    }
}