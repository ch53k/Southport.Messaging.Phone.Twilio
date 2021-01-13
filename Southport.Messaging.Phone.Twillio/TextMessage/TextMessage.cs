﻿using System;
using System.Threading.Tasks;
using Southport.Messaging.Phone.Twilio.Shared;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using HttpClient = System.Net.Http.HttpClient;

namespace Southport.Messaging.Phone.Twilio.TextMessage
{
    public class TextMessage : TwilioClientBase, ITextMessage
    {

        public TextMessage(HttpClient httpClient, ITwilioOptions twilioOptions) : base(httpClient, twilioOptions)
        {
        }


        public async Task<MessageResource> SendAsync(string to, string message, string from, string messageServiceSid)
        {
            if (string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("To phone number cannot be null or empty.", nameof(to));
            }
            to = TwilioHelper.NormalizePhoneNumber(to);

            if (IsSandboxed == false && string.IsNullOrWhiteSpace(from))
            {
                throw new ArgumentException("From phone number cannot be null or empty.", nameof(from));
            }
            from = IsSandboxed ? "+15005550006" : from;

            var messageResponse = await MessageResource.CreateAsync(
                new PhoneNumber(to),
                @from: new PhoneNumber(from),
                body: message,
                messagingServiceSid: messageServiceSid,
                client: _innerClient); // pass in the custom client

            return messageResponse;
        }


        
    }
}
