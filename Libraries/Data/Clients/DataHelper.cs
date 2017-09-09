using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalendar.Zero.DB.Entity.Base;
using Kalendar.Zero.Utility.Common;

namespace Kalendar.Zero.Data.Clients
{
    public class DataHelper
        : BaseHelper, IBaseHelper
    {
        private ApiTerminal.Entities.Channel ProxyChannel
        {
            get
            {
                var channel=new ApiTerminal.Entities.Channel();
                channel.FillFrom(Channel);
                return channel;
            }
        }

        private ApiTerminal.Entities.Avatar ProxyAvatar
        {
            get
            {
                var avatar = new ApiTerminal.Entities.Avatar();
                avatar.FillFrom(Avatar);
                return avatar;
            }
        }

        public string Signin()
        {
            var request = new ApiTerminal.Clients.Request.WoowRequest
            {
                ChannelSymbol = Channel.ChannelSymbol,
                Method = "Signin",
                Channel = ProxyChannel,
                Avatar= ProxyAvatar,

            };

            return ReadFromProxy(request).JsonToObj<string>();
        }

        public AccountAvatarsPO ExchangeToken(string code)
        {
            return new AccountAvatarsPO();
        }

        public AccountAvatarsPO RefreshToken(string refreshToken)
        {
            return new AccountAvatarsPO();
        }

        public AccountAvatarsPO ReadAvatar()
        {
            return new AccountAvatarsPO();
        }

        public List<AccountMessagesPO> ReadMessages(int page = 1)
        {
            return new List<AccountMessagesPO>();
        }

        public List<AccountContactsPO> ReadContacts(int page = 1)
        {
            return new List<AccountContactsPO>();
        }

        public List<SchedulePO> ReadSchedules(int page = 1)
        {
            return new List<SchedulePO>();
        }

        public SchedulePO CreateSchedules(SchedulePO schedule)
        {
            return new SchedulePO();
        }

        public bool CancelSchedules(SchedulePO schedule)
        {
            return true;
        }

        public SchedulePO UpdateSchedules(SchedulePO schedule)
        {
            return new SchedulePO();
        }

        private string ReadFromProxy(ApiTerminal.Clients.Request.WoowRequest request)
        {
            var r = new Utility.Common.BrowserClient();
            return r.SendHttpRequest(Config.ProxyApiUri, false, "POST", request.ObjToJson(), null, null, null, "UTF-8", "application/json", "application/json");
        }
    }
}
