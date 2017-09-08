using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Clients.Response
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DataContract]
    public class MsonlineToken
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// 过期时长(秒)
        /// </summary>
        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "ext_expires_in")]
        public int ExtExpiresIn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "scope")]
        public string Scope { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}
