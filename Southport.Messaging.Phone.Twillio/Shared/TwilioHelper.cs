namespace Southport.Messaging.Phone.Twilio.Shared
{
    public static class TwilioHelper
    {
        public static string NormalizePhoneNumber(string phoneNumber)
        {
            
            if (phoneNumber.StartsWith("1") == false)
            {
                phoneNumber = $"1{phoneNumber}";
            }
            if (phoneNumber.StartsWith("+") == false)
            {
                phoneNumber = $"+{phoneNumber}";
            }

            return phoneNumber;
        }
    }
}
