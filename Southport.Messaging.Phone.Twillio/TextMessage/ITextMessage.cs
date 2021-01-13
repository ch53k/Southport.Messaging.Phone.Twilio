using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace Southport.Messaging.Phone.Twilio.TextMessage
{
    public interface ITextMessage
    {
        Task<MessageResource> SendAsync(string to, string message, string from, string messageServiceSid);
    }
}