﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Kalendar.Zero.ApiTerminal.Clients
{
    public class MsonlineHelper
        :BaseHelper,IBaseHelper
    {
        /// <summary>
        /// 登录地址
        /// </summary>
        /// <returns></returns>
        public new string Signin()
        {
            var url =
                string.Format(
                    "https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id={0}&redirect_uri={1}&response_type=code&scope={2}",
                    Channel.AppId,
                    Channel.CodeCallback,
                    Channel.Parameters
                    );

            return url;
        }
        
        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public new Entities.Avatar ExchangeToken(string code)
        {
            var now = DateTime.Now;
            var avatar = Avatar ?? new Entities.Avatar();
            var url = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

            var data =
                string.Format(
                    "grant_type=authorization_code&code={0}&redirect_uri={1}&client_id={2}&client_secret={3}",
                    code,
                    HttpUtility.UrlEncode(Channel.CodeCallback),
                    Channel.AppId,
                    Channel.AppSecret);

            try
            {
                var r = new BrowserClient();
                var response = r.SendHttpRequest(url,true,"POST", data,null,null,null,"utf-8","application/json", "application/x-www-form-urlencoded");
                Logger.Info(response);
                var token = response.JsonToObjContract<Response.MsonlineToken>();
                if (token != null)
                {
                    avatar.ChannelId = Channel.Id;
                    avatar.Token = token.AccessToken;
                    avatar.RefreshToken = token.RefreshToken;
                    avatar.TokenGenerated = now;
                    avatar.TokenExpires = now.AddSeconds(token.ExpiresIn);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return avatar;
        }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public new Entities.Avatar RefreshToken(string refreshToken)
        {
            var now = DateTime.Now;
            var avatar = Avatar ?? new Entities.Avatar();
            var url = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

            Logger.Info(Channel.SerializeXml());

            var data =
                string.Format(
                    "grant_type=refresh_token&refresh_token={0}&redirect_uri={1}&client_id={2}&client_secret={3}&scope={4}",
                    refreshToken,
                    HttpUtility.UrlEncode(Channel.CodeCallback),
                    Channel.AppId,
                    Channel.AppSecret,
                    Channel.Parameters);
            Logger.Info("data="+data);
            try
            {
                var r = new BrowserClient();
                var response = r.SendHttpRequest(url, true, "POST", data, null, null, null, "utf-8", "application/json", "application/x-www-form-urlencoded");
                Logger.Info(response);
                var token = response.JsonToObjContract<Response.MsonlineToken>();
                if (token != null)
                {
                    avatar.ChannelId = Channel.Id;
                    avatar.Token = token.AccessToken;
                    avatar.RefreshToken = token.RefreshToken;
                    avatar.TokenGenerated = now;
                    avatar.TokenExpires = now.AddSeconds(token.ExpiresIn);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return avatar;
        }

        public new Entities.Avatar ReadAvatar()
        {
            var now = DateTime.Now;
            var avatar = Avatar ?? new Entities.Avatar();
            var url = "https://graph.microsoft.com/v1.0/me";

            var response = ReadApi(url);

            var user = response.JsonToObjContract<Response.MsonlineUser>();
            if (user != null)
            {
                avatar.ChannelIdentity = user.Id;
                avatar.ChannelId = Channel.Id;
                avatar.DisplayName =string.IsNullOrEmpty(user.DisplayName)?user.UserPrincipalName: user.DisplayName;
                avatar.Code = user.UserPrincipalName;
            }
            
            return avatar;
        }

        public new List<Entities.Message> ReadMessages(int page = 1)
        {
            var result= new List<Entities.Message>();
            var url = "https://graph.microsoft.com/v1.0/me/mailfolders/inbox/messages";
            url += "?$select=id,receivedDateTime,subject,importance,webLink,sender";
            if (page > 1)
            {
                url += "&$skip=" + (page - 1) * 10;
            }

            try
            {
                var response = ReadApi(url);

                var messages = response.JsonToObjContract<Response.MsonlineMessages>();
                //messages.hasNext = response.Contains("@odata.nextLink");
                Logger.Info(messages.SerializeXml());

                result.AddRange(
                    messages.Data.Select(
                        msonlineMessage => new Entities.Message
                        {
                            AvatarId=Avatar.Id,
                            ChannelIdentity = msonlineMessage.Id,
                            //MessageType = (int) DB.Defined.MessageType.Mail,
                            MessageSubject = msonlineMessage.Sender.EmailAddress.Name + " " + msonlineMessage.Subject,
                            MessageContent = msonlineMessage.ObjToJson(),
                            Weblink = msonlineMessage.WebLink,
                            CreateTime=msonlineMessage.ReceivedDateTime.ToDateTime()
                        }));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return result;
        }

        public new List<Entities.Contact> ReadContacts(int page=1)
        {
            var result= new List<Entities.Contact>();
            var url = "https://graph.microsoft.com/v1.0/me/contacts";

            url += "?$select=id,displayName,homePhones,mobilePhone,birthday";

            if (page > 1)
            {
                url += "&$skip=" + (page - 1)*10;
            }

            try
            {
                var response = ReadApi(url);

                var contacts = response.JsonToObjContract<Response.MsonlineContacts>();
                //contacts.hasNext = response.Contains("@odata.nextLink");

                Logger.Info(contacts.SerializeXml());

                result.AddRange(
                    contacts.Data.Select(
                        msonlineContact=> new Entities.Contact
                        {
                            AvatarId = Avatar.Id,
                            ChannelIdentity = msonlineContact.Id,
                            DisplayName = msonlineContact.DisplayName,
                            Detail = msonlineContact.ObjToJson()
                        }));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return result;
        }

        public new List<Entities.Event> ReadEvents(int page = 1)
        {
            var result= new List<Entities.Event>();
            var url = "https://graph.microsoft.com/v1.0/me/calendar/events";

            url += "?$select=id,subject,bodyPreview,start,end,location,showAs,webLink,recurrence";

            if (page > 1)
            {
                url += "&$skip=" + (page - 1)*10;
            }

            try
            {
                var response = ReadApi(url);

                var events = response.JsonToObjContract<Response.MsonlineEvents>();
                //events.hasNext = response.Contains("@odata.nextLink");
                Logger.Info(events.SerializeXml());

                result.AddRange(
                    events.Data.Select(
                        msonlineEvent => new Entities.Event
                        {
                            ChannelIdentity = msonlineEvent.Id,
                            Cycle =
                                msonlineEvent.Recurrence != null && msonlineEvent.Recurrence.Pattern != null
                                    ? GetMsonlineCycle(msonlineEvent.Recurrence.Pattern.Type)
                                    : 0,
                            EventSubject= msonlineEvent.Subject,
                            EventLead= msonlineEvent.BodyPreview,
                            Start = msonlineEvent.Start.DateTime.ToDateTime(),
                            End = msonlineEvent.End.DateTime.ToDateTime(),
                            Weblink = msonlineEvent.WebLink
                        }));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }


            return result;
        }

        public new Entities.Event CreateEvent(Entities.Event eventInfo)
        {
            
            return eventInfo;
        }

        public new bool CancelEvent(Entities.Event eventInfo)
        {
            var result = true;


            return result;
        }

        public new Entities.Event UpdateEvent(Entities.Event eventInfo)
        {

            return eventInfo;
        }


        }
}