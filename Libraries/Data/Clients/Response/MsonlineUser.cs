using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.Data.Clients.Response
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DataContract]
    public class MsonlineUser
    {
        /*
            "givenName": "Karl",
            "surname": "Zhao",
            "displayName": "Karl Zhao",
            "id": "6cd2df35ca5f55ac",
            "userPrincipalName": "zhaoma@hotmail.com",
            "businessPhones": [],
            "jobTitle": null,
            "mail": null,
            "mobilePhone": null,
            "officeLocation": null,
            "preferredLanguage": null
             */

        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }


        [DataMember(Name = "id")]
        public string Id { get; set; }


        [DataMember(Name = "userPrincipalName")]
        public string UserPrincipalName { get; set; }
    }
}
