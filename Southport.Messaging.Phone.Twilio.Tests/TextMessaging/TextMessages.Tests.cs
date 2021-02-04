using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Southport.Messaging.Phone.Twilio.TextMessage;
using Xunit;

namespace Southport.Messaging.Phone.Twilio.Tests.TextMessaging
{
    public class TextMessageTests
    {
        private ITextMessage TextMessage { get; }
        public TextMessageTests()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var options = Startup.GetOptions();

            fixture.Register(()=>options);
            fixture.Register(()=> new HttpClient());

            TextMessage = fixture.Create<TextMessage.TextMessage>();
        }

        [Fact]
        public async Task Send_BadFromNumber()
        {
            var toNumber = "+15555551212";
            var fromNumber = "+15005550001";
            var message = "Testing";
            var response = await TextMessage.SendAsync(toNumber, message, fromNumber);

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
            var response = await TextMessage.SendAsync(toNumber, message, fromNumber);
            

            Assert.True(response.IsSuccessful);
        }
    }
}
