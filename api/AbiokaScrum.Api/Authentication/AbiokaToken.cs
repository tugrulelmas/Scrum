﻿using AbiokaScrum.Api.Entities;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;

namespace AbiokaScrum.Api.Authentication
{
    public class AbiokaToken
    {
        private const string key = "_A4b%i+oKa$_";

        public static string Encode(UserInfo userInfo)
        {
            var defaultExpMinutes = new TimeSpan(240, 0, 0).TotalMinutes;
            return Encode(userInfo, defaultExpMinutes);
        }

        public static string Encode(UserInfo userInfo, double expirationMinutes)
        {
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var issueTime = DateTime.UtcNow;

            var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            var exp = (int)issueTime.AddMinutes(expirationMinutes).Subtract(utc0).TotalSeconds;

            var payload = new TokenPayload
            {
                iss = "abioka",
                exp = exp,
                iat = iat,
                name = userInfo.Name,
                email = userInfo.Email,
                id = userInfo.Id,
                image_url = userInfo.ImageUrl,
                initials = userInfo.Initials,
                provider = userInfo.Provider.ToString()
            };

            return JsonWebToken.Encode(payload, key, JwtHashAlgorithm.HS256);
        }

        public static TokenPayload Decode(string token)
        {
            var str = JsonWebToken.Decode(token, key);
            var payload = JsonConvert.DeserializeObject<TokenPayload>(str);
            return payload;
        }
    }

    public class TokenPayload
    {
        public string iss { get; set; }

        public int exp { get; set; }

        public int iat { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public Guid id { get; set; }

        public string image_url { get; set; }

        public string initials { get; set; }

        public string provider { get; set; }

        public string language { get; set; }
    }
}