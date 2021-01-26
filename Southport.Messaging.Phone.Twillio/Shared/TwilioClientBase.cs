using Twilio.Clients;
using Twilio.Http;
using HttpClient = System.Net.Http.HttpClient;

namespace Southport.Messaging.Phone.Twilio.Shared
{
    public abstract class TwilioClientBase
    {
        protected  readonly TwilioRestClient _innerClient;

        public bool UseSandbox { get; }

        protected TwilioClientBase(HttpClient httpClient, string accountSid, string apiKey, string authToken, bool useSandbox)
        {
            UseSandbox = useSandbox;

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

        protected TwilioClientBase(HttpClient httpClient, ITwilioOptions options) : this(httpClient, options.AccountSid, options.ApiKey, options.AuthToken, options.UseSandbox)
        {
        }
    }
}
