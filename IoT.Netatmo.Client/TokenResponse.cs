using System;
using System.Runtime.Serialization;

namespace IoT.Netatmo.Client
{
    [DataContract]
    public class TokenResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}