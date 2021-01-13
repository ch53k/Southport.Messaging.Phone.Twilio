using Twilio.Clients;
using Twilio.Http;
using HttpClient = System.Net.Http.HttpClient;

namespace Southport.Messaging.Phone.Twilio.Shared
{
    public abstract class TwilioClientBase
    {
        protected readonly ITwilioOptions _twilioOptions;
        protected  readonly TwilioRestClient _innerClient;

        public bool IsSandboxed => _twilioOptions.IsSandboxed;

        protected TwilioClientBase(HttpClient httpClient, ITwilioOptions twilioOptions)
        {
            
            _twilioOptions = twilioOptions;

            if (IsSandboxed)
            {
                _innerClient = new TwilioRestClient(
                    twilioOptions.AccountSid,
                    twilioOptions.AuthToken,
                    httpClient: new SystemNetHttpClient(httpClient));
            }
            else
            {
                _innerClient = new TwilioRestClient(
                    twilioOptions.ApiKey,
                    twilioOptions.AuthToken,
                    httpClient: new SystemNetHttpClient(httpClient),
                    accountSid: twilioOptions.AccountSid);
            }
        }
    }
}
