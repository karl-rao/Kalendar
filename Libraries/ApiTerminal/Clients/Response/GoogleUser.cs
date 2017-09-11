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
    public class GoogleUser
    {
        /*
{
    "id": "101041433066104582113",
    "name": "赵马",
    "given_name": "马",
    "family_name": "赵",
    "link": "https://plus.google.com/101041433066104582113",
    "picture": "https://lh6.googleusercontent.com/-aQIfycAFfeU/AAAAAAAAAAI/AAAAAAAAAII/dbmnSFPzJk0/photo.jpg",
    "gender": "male",
    "locale": "zh-CN"
}
             */

        [DataMember(Name = "name")]
        public string Name { get; set; }


        [DataMember(Name = "id")]
        public string Id { get; set; }


        [DataMember(Name = "picture")]
        public string Picture { get; set; }
    }
}
