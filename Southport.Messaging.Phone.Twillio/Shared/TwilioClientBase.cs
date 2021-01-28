using System.Collections.Generic;
using Twilio.Clients;
using Twilio.Http;
using HttpClient = System.Net.Http.HttpClient;

namespace Southport.Messaging.Phone.Twilio.Shared
{
    public abstract class TwilioClientBase
    {
        protected  readonly TwilioRestClient _innerClient;

        public bool UseSandbox { get; }
        public List<string> TestPhoneNumbers { get; } = new List<string>();

        protected TwilioClientBase(HttpClient httpClient, string accountSid, string apiKey, string authToken, bool useSandbox, string testPhoneNumber = null)
        {
            UseSandbox = useSandbox;

            if (string.IsNullOrWhiteSpace(testPhoneNumber) == false)
            {
                TestPhoneNumbers.AddRange(testPhoneNumber.Split(','));
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

        protected TwilioClientBase(HttpClient httpClient, ITwilioOptions options) : this(httpClient, options.AccountSid, options.ApiKey, options.AuthToken, options.UseSandbox, options.TestPhoneNumbers)
        {
        }
    }
}
