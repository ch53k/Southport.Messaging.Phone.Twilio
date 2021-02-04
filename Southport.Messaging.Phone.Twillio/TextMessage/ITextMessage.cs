using System.Threading.Tasks;
using Southport.Messaging.Phone.Twilio.TextMessage.Response;

namespace Southport.Messaging.Phone.Twilio.TextMessage
{
    public interface ITextMessage
    {
        Task<ITextMessageResponse> SendAsync(string to, string message, string from, string messageServiceSid = null);
    }
}