using System;
using System.Linq;
using System.Threading.Tasks;
using Southport.Messaging.Phone.Twilio.Shared;
using Southport.Messaging.Phone.Twilio.TextMessage.Response;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using HttpClient = System.Net.Http.HttpClient;

namespace Southport.Messaging.Phone.Twilio.TextMessage
{
    public class TextMessage : TwilioClientBase, ITextMessage
    {
        public TextMessage(HttpClient httpClient, ITwilioOptions options) : base(httpClient, options)
        {
        }

        public TextMessage(HttpClient httpClient, string accountSid, string apiKey, string authToken, bool useSandbox = false, string testPhoneNumbers = null) : base(httpClient, accountSid, apiKey, authToken, useSandbox, testPhoneNumbers)
        {
        }


        public async Task<ITextMessageResponse> SendAsync(string to, string message, string from, string messageServiceSid = null)
        {
            if (TestPhoneNumbers.Any())
            {
                return await SendTestPhoneNumbersAsync(message, from, messageServiceSid);
            }

            if (string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("To phone number cannot be null or empty.", nameof(to));
            }
            to = TwilioHelper.NormalizePhoneNumber(to);

            
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (UseSandbox == false && string.IsNullOrWhiteSpace(from))
            {
                throw new ArgumentException("From phone number cannot be null or empty.", nameof(from));
            }

            try
            {
                var messageResponse = await MessageResource.CreateAsync(
                    new PhoneNumber(to),
                    @from: new PhoneNumber(from),
                    body: message,
                    messagingServiceSid: messageServiceSid,
                    client: _innerClient); // pass in the custom client

                return (TextMessageResponse) messageResponse;
            }
            catch (ApiException e)
            {
                return TextMessageResponse.Failed(e.Message, e.MoreInfo, e.Code);
            }

        }

        private async Task<ITextMessageResponse> SendTestPhoneNumbersAsync(string message, string from, string messageServiceSid = null)
        {
            TextMessageResponse messageResponse = null;
            foreach (var phoneNumber in TestPhoneNumbers)
            {
                var to = TwilioHelper.NormalizePhoneNumber(phoneNumber);

                if (UseSandbox == false && string.IsNullOrWhiteSpace(from))
                {
                    throw new ArgumentException("From phone number cannot be null or empty.", nameof(from));
                }

                from = UseSandbox ? "+15005550006" : from;

                var twilioResponse = await MessageResource.CreateAsync(
                    new PhoneNumber(to),
                    @from: new PhoneNumber(from),
                    body: message,
                    messagingServiceSid: messageServiceSid,
                    client: _innerClient); // pass in the custom client

                messageResponse = (TextMessageResponse) twilioResponse;
            }


            return (TextMessageResponse) messageResponse;
        }


        
    }
}
