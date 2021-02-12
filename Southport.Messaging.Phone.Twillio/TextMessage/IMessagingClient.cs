using System.Threading.Tasks;
using Southport.Messaging.Phone.Twilio.TextMessage.Response;

namespace Southport.Messaging.Phone.Twilio.TextMessage
{
    public interface ITwilioTextMessage
    {
        string From { get; set;  }
        string To { get; set; }
        string MessageServiceSid { get; set; }
        string Message { get; set; }

        ITwilioTextMessage SetFrom(string from);
        ITwilioTextMessage SetTo (string to);
        ITwilioTextMessage SetMessageServicesSid(string messageServiceSid);
        ITwilioTextMessage SetMessage(string message);
        Task<ITextMessageResponse> SendAsync();
    }
}