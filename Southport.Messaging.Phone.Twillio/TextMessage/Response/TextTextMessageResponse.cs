﻿using System;
using System.Collections.Generic;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Southport.Messaging.Phone.Twilio.TextMessage.Response
{
    public class TextTextMessageResponse : ITextMessageResponse
    {

        public static explicit operator TextTextMessageResponse(MessageResource b) => new(b);

        TextTextMessageResponse(MessageResource messageResource)
        {
            Body = messageResource.Body;

            NumSegments = messageResource.NumSegments;

            Direction = messageResource.Direction.ToString();

            From = messageResource.From;

            To = messageResource.To;

            DateUpdated = messageResource.DateUpdated;

            Price = messageResource.Price;

            ErrorMessage = messageResource.ErrorMessage;

            Uri = messageResource.Uri;

            AccountSid = messageResource.AccountSid;

            NumMedia = messageResource.NumMedia;

            Status = messageResource.Status.ToString();

            MessagingServiceSid = messageResource.MessagingServiceSid;

            Sid = messageResource.Sid;

            DateSent = messageResource.DateSent;

            DateCreated = messageResource.DateCreated;

            ErrorCode = messageResource.ErrorCode;

            PriceUnit = messageResource.PriceUnit;

            ApiVersion = messageResource.ApiVersion;

            SubresourceUris = messageResource.SubresourceUris;
        }

        public string Body { get; }
        public string NumSegments { get; }
        public DirectionEnum Direction { get; }
        public PhoneNumber From { get; }
        public string To { get; }
        public DateTime? DateUpdated { get; }
        public string Price { get; }
        public string ErrorMessage { get; }
        public string Uri { get; }
        public string AccountSid { get; }
        public string NumMedia { get; }
        public StatusEnum Status { get; }
        public string MessagingServiceSid { get; }
        public string Sid { get; }
        public DateTime? DateSent { get; }
        public DateTime? DateCreated { get; }
        public int? ErrorCode { get; }
        public string PriceUnit { get; }
        public string ApiVersion { get; }
        public Dictionary<string, string> SubresourceUris { get; }
    }
}
