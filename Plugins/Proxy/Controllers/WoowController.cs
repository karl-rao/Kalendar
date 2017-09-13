using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kalendar.Zero.ApiTerminal.Clients;

namespace Kalendar.Plugins.Proxy.Controllers
{
    public class WoowController : ApiController
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: api/Woow/5
        public string Get(int id)
        {
            return id+"";
        }

        // POST: api/Woow
        public object Post([FromBody]Zero.ApiTerminal.Clients.Request.WoowRequest request)
        {
            object result=null;

            Logger.Info("PARSE REQUEST:"+request.SerializeXml());
            BaseHelper helper=null;
            switch (request.ChannelSymbol)
            {
                case 1:
                    helper=new MsonlineHelper();
                    break;
                case 2:
                    helper=new GoogleHelper();
                    break;
            }
            if (helper != null)
            {
                helper.Channel = request.Channel;
                helper.Avatar = request.Avatar;
            }
            else
            {
                return "";
            }

            Logger.Debug(helper.GetType());

            switch ((request.Method+"").ToUpper())
            {
                case "SIGNIN":
                    result = helper.Signin();
                    break;
                case "EXCHANGETOKEN":
                    result = helper.ExchangeToken(request.Data);
                    break;
                case "REFRSHTOKEN":
                    result = helper.RefreshToken(request.Data);
                    break;
                case "READAVATAR":
                    result = helper.ReadAvatar();
                    break;
                case "READMESSAGES":
                    Logger.Debug("READMESSAGES");
                    result = helper.ReadMessages(request.Data.ToInt());
                    break;
                case "READCONTACTS":
                    Logger.Debug("READCONTACTS");
                    result = helper.ReadContacts(request.Data.ToInt());
                    break;
                case "READEVENTS":
                    Logger.Debug("READEVENTS");
                    result = helper.ReadEvents(request.Data.ToInt());
                    break;
                case "CREATEEVENT":
                    result = helper.CreateEvent(request.Data.JsonToObj<Zero.ApiTerminal.Entities.Event>());
                    break;
                case "CANCELEVENT":
                    result = helper.CancelEvent(request.Data.JsonToObj<Zero.ApiTerminal.Entities.Event>());
                    break;
                case "UPDATEEVENT":
                    result = helper.UpdateEvent(request.Data.JsonToObj<Zero.ApiTerminal.Entities.Event>());
                    break;
            }

            Logger.Info("CALLBACK:"+result);

            return result;
        }
    }
}
