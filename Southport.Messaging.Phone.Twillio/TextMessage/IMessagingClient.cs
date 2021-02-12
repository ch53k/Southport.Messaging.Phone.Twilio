using System.Threading.Tasks;
using Southport.Messaging.Phone.Twilio.TextMessage.Response;

namespace Southport.Messaging.Phone.Twilio.TextMessage
{
    public interface ITwilioMTextMessage
    {
        string From { get; set;  }
        string To { get; set; }
        string MessageServiceSid { get; set; }
        string Message { get; set; }

        ITwilioMTextMessage SetFrom(string from);
        ITwilioMTextMessage SetTo (string to);
        ITwilioMTextMessage SetMessageServicesSid(string messageServiceSid);
        ITwilioMTextMessage SetMessage(string message);
        Task<ITextMessageResponse> SendAsync();
    }
}