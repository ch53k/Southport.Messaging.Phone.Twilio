using System.Collections.Generic;
using Twilio.Clients;
using Twilio.Http;
using HttpClient = System.Net.Http.HttpClient;

namespace Southport.Messaging.Phone.Twilio.Shared
{
    public abstract class TwilioClientBase
    {
        protected  readonly TwilioRestClient _innerClient;

        protected string MessagingServiceSid { get; }
        public bool UseSandbox { get; }
        public List<string> TestPhoneNumbers { get; } = new();

        protected TwilioClientBase(HttpClient httpClient, string accountSid, string apiKey, string authToken, string messagingServiceSid, bool useSandbox, string testPhoneNumbers = null)
        {
            MessagingServiceSid = messagingServiceSid;
            UseSandbox = useSandbox;

            if (string.IsNullOrWhiteSpace(testPhoneNumbers) == false)
            {
                TestPhoneNumbers.AddRange(testPhoneNumbers.Split(','));
            }

            if (UseSandbox)
            {
                _innerClient = new TwilioRestClient(
                    accountSid,
                    authToken,
                    httpClient: new SystemNetHttpClient(httpClient));
            }
            else
            {
                _innerClient = new TwilioRestClient(
                    apiKey,
                    authToken,
                    httpClient: new SystemNetHttpClient(httpClient),
                    accountSid: accountSid);
            }
        }

        protected TwilioClientBase(HttpClient httpClient, ITwilioOptions options) : this(httpClient, options.AccountSid, options.ApiKey, options.AuthToken, options.MessagingServiceSid, options.UseSandbox, options.TestPhoneNumbers)
        {
        }
    }
}
