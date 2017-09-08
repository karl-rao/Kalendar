using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Kalendar.Utility.Common
{
    /// <summary>
    /// 字节编码
    /// </summary>
    public class Rijndael : IHttpModule
    {
          private static Boolean _active;
          #region IHttpModule Members

          /// <summary>
          /// 处置由实现 <see cref="T:System.Web.IHttpModule"/> 的模块使用的资源（内存除外）。
          /// </summary>
          public void Dispose() { }

          /// <summary>
          /// 初始化模块，并使其为处理请求做好准备。
          /// </summary>
          /// <param name="context">一个 <see cref="T:System.Web.HttpApplication"/>，它提供对 ASP.NET 应用程序内所有应用程序对象的公用的方法、属性和事件的访问</param>
          public void Init(HttpApplication context)
          {
              context.BeginRequest += Application_BeginRequest;
              _active = true;
          }

          #endregion

          /// <summary>
          /// Handles the BeginRequest event of the Application control.
          /// </summary>
          /// <param name="sender">The source of the event.</param>
          /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
          public void Application_BeginRequest(object sender, EventArgs args)
          {
              if (HttpContext.Current.Request.QueryString["ck"] != null)
              {
                  String criptedQueryString = HttpContext.Current.Request.QueryString["ck"];
                  Byte[] rawQueryString = Convert.FromBase64String(criptedQueryString);
                  var ms = new MemoryStream();
                  var crypto = new RijndaelManaged();
                  ICryptoTransform ct = crypto.CreateDecryptor(
                      HexEncoding.GetBytes(Config.QueryStringEncryptionKey ),
                      HexEncoding.GetBytes(Config.InitializationVector ));
                  var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                  cs.Write(rawQueryString, 0, rawQueryString.Length);
                  cs.Close();
                  String decryptedQueryString = Encoding.ASCII.GetString(ms.ToArray());
                  HttpContext.Current.RewritePath(HttpContext.Current.Request.Path + "?" + decryptedQueryString);
              }
              else if (HttpContext.Current.Request.QueryString.Count > 0)
              {
                  throw new SecurityException("Wrong querystring");
              }
          }

          /// <summary>
          /// Encodes the query string.
          /// </summary>
          /// <param name="queryString">The query string.</param>
          /// <returns></returns>
          public static String EncodeQueryString(String queryString)
          {
              if (!_active) return queryString;
              var ms = new MemoryStream();
              var crypto = new RijndaelManaged();
              ICryptoTransform ct = crypto.CreateEncryptor(
               HexEncoding.GetBytes(Config.QueryStringEncryptionKey),
               HexEncoding.GetBytes(Config.InitializationVector));
              var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
              Byte[] rawQueryString = Encoding.ASCII.GetBytes(queryString);
              cs.Write(rawQueryString, 0, rawQueryString.Length);
              cs.Close();
              return "ck=" + HttpContext.Current.Server.UrlEncode(Convert.ToBase64String(ms.ToArray()));
          }

          /// <summary>
          /// Encodes the link.
          /// </summary>
          /// <param name="link">The link.</param>
          /// <returns></returns>
          public static String EncodeLink(String link)
          {
              if (link.Contains("?") && _active)
              {
                  String[] linkpart = link.Split('?');
                  if (!linkpart[1].StartsWith("ck"))
                  {
                      return linkpart[0] + "?" + EncodeQueryString(linkpart[1]);
                  }
              }
              return link;
          }

          /// <summary>
          /// Gets the machine key.
          /// </summary>
          /// <returns></returns>
          public static Byte[] GetMachineKey()
          {
              var section = (MachineKeySection)WebConfigurationManager.GetSection("system.web/machineKey");
              return HexEncoding.GetBytes(section.DecryptionKey);
          } 
    }
}
