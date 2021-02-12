﻿using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Southport.Messaging.Phone.Twilio.TextMessage;
using Xunit;
using Xunit.Abstractions;

namespace Southport.Messaging.Phone.Twilio.Tests.TextMessaging
{
    public class TextMessageTests
    {
        private readonly ITestOutputHelper _output;
        private ITwilioTextMessage TwilioTextMessage { get; }
        public TextMessageTests(ITestOutputHelper output)
        {
            _output = output;
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var options = Startup.GetOptions();

            fixture.Register(()=>options);
            fixture.Register(()=> new HttpClient());

            TwilioTextMessage = fixture.Create<TwilioTextMessage>();
            TwilioTextMessage.MessageServiceSid = null;
        }

        [Fact]
        public async Task Send_BadFromNumber()
        {
            var toNumber = "+15555551212";
            var fromNumber = "+15005550001";
            var message = "Testing";

            var response = await TwilioTextMessage
                .SetFrom(fromNumber)
                .SetTo(toNumber)
                .SetMessage(message)
                .SendAsync();

            var expectedErrorCode = 21212;

            Assert.False(response.IsSuccessful);
            Assert.Equal(expectedErrorCode, response.ErrorCode);
            Assert.False(string.IsNullOrWhiteSpace(response.ErrorMessage));
            Assert.False(string.IsNullOrWhiteSpace(response.MoreInfo));
        }

        [Fact]
        public async Task Send_Success()
        {
            var toNumber = "+15155551212";
            var fromNumber = "+15005550006";
            var message = "Testing";

            var response = await TwilioTextMessage
                .SetFrom(fromNumber)
                .SetTo(toNumber)
                .SetMessage(message)
                .SendAsync();
            

            Assert.True(response.IsSuccessful);
        }
    }
}
