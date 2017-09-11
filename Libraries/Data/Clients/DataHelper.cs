using System;
using System.Collections.Generic;
using Kalendar.Zero.DB.Entity.Base;
using Kalendar.Zero.Utility.Common;

namespace Kalendar.Zero.Data.Clients
{
    public class DataHelper
        : BaseHelper, IBaseHelper
    {
        #region private
        private ApiTerminal.Clients.Request.WoowRequest ProxyRequest(string method)
        {
            var channel = new ApiTerminal.Entities.Channel();
            channel.FillFrom(Channel);

            var avatar = new ApiTerminal.Entities.Avatar();
            avatar.FillFrom(Avatar);

            return new ApiTerminal.Clients.Request.WoowRequest
            {
                ChannelSymbol = Channel.ChannelSymbol,
                Method = method,
                Channel = channel,
                Avatar = avatar
            };
        }
        
        private string ReadFromProxy(ApiTerminal.Clients.Request.WoowRequest request)
        {
            var r = new BrowserClient();
            return r.SendHttpRequest(Config.ProxyApiUri, false, "POST", request.ObjToJson(), null, null, null, "UTF-8", "application/json", "application/json");
        }

        #endregion

        public string Signin()
        {
            var request = ProxyRequest("Signin");

            return ReadFromProxy(request).JsonToObj<string>();
        }

        public AccountAvatarsPO ExchangeToken(string code)
        {
            var request = ProxyRequest("ExchangeToken");
            request.Data = code;

            return ReadFromProxy(request).JsonToObj<AccountAvatarsPO>();
        }

        public AccountAvatarsPO RefreshToken(string refreshToken)
        {
            var request = ProxyRequest("RefreshToken");
            request.Data = refreshToken;

            return ReadFromProxy(request).JsonToObj<AccountAvatarsPO>();
        }

        public AccountAvatarsPO ReadAvatar()
        {
            var request = ProxyRequest("ReadAvatar");

            return ReadFromProxy(request).JsonToObj<AccountAvatarsPO>();
        }

        public List<AccountMessagesPO> ReadMessages(int page = 1)
        {
            var request = ProxyRequest("ReadMessages");
            request.Data = page+"";

            return ReadFromProxy(request).JsonToObj<List<AccountMessagesPO>>();
        }

        public List<AccountContactsPO> ReadContacts(int page = 1)
        {
            var request = ProxyRequest("ReadContacts");
            request.Data = page + "";

            return ReadFromProxy(request).JsonToObj<List<AccountContactsPO>>();
        }

        public List<SchedulePO> ReadSchedules(int page = 1)
        {
            var request = ProxyRequest("ReadSchedules");
            request.Data = page + "";

            return ReadFromProxy(request).JsonToObj<List<SchedulePO>>();
        }

        public SchedulePO CreateSchedules(SchedulePO schedule)
        {
            var request = ProxyRequest("CreateSchedules");
            request.Data = schedule.ObjToJson();

            return ReadFromProxy(request).JsonToObj<SchedulePO>();
        }

        public bool CancelSchedules(SchedulePO schedule)
        {
            var request = ProxyRequest("CancelSchedules");
            request.Data = schedule.ObjToJson();

            return ReadFromProxy(request).JsonToObj<bool>();
        }

        public SchedulePO UpdateSchedules(SchedulePO schedule)
        {
            var request = ProxyRequest("UpdateSchedules");
            request.Data = schedule.ObjToJson();

            return ReadFromProxy(request).JsonToObj<SchedulePO>();
        }

    }
}
