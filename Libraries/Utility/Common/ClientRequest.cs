using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;

namespace Kalendar.Utility.Common
{
	/// <summary>
    /// 客户端请求类
    /// </summary>
    public class ClientRequest
    {
		/// <summary>
        /// Gets or sets the client cookie container.
        /// </summary>
        /// <value>
        /// The client cookie container.
        /// </value>
        public CookieContainer ClientCookieContainer { get; set; }
		/// <summary>
        /// Initializes a new instance of the <see cref="ClientRequest"/> class.
        /// </summary>
        public ClientRequest()
        {
            ClientCookieContainer = new CookieContainer();
        }

        #region Get请求
		/// <summary>
        /// Sends the HTTP request get.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public string SendHttpRequestGet(string url, Encoding encoding )
        {
            url=UrlByVerification(url);
            encoding = EncodingByVerification(encoding);

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded; charset=" + encoding.WebName;
            request.CookieContainer = ClientCookieContainer;
            using (WebResponse response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        #endregion

        #region  Post请求（不含文件上传功能）
		/// <summary>
        /// Sends the HTTP request post.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public string SendHttpRequestPost(string url, Dictionary<string, string> keyValue, Encoding encoding)
        {
            if (keyValue == null)
            {
                SendHttpRequestGet(url, encoding);
            }

            url = UrlByVerification(url);
            encoding = EncodingByVerification(encoding);
            
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=" + encoding.WebName;
 
            byte[] buffer=null;
            if (keyValue != null && keyValue.Count > 0)
            {
                string postData = null;
                postData = string.Join("&",
                    (from kvp in keyValue
                     let item = kvp.Key + "=" + HttpUtility.UrlEncode(kvp.Value)
                     select item
                          ).ToArray()
                    );
 
                buffer = encoding.GetBytes(postData);
                request.CookieContainer = ClientCookieContainer;
                Stream stream = request.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
            }
            
            using (WebResponse response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        #endregion

        #region Post请求 
		/// <summary>
        /// Sends the HTTP request post.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="fileList">The file list.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="cookieContainer1">The cookie container1.</param>
        /// <returns></returns>
        public string SendHttpRequestPost(string url, Dictionary<string, string> keyValue,
            Dictionary<string, string> fileList, Encoding encoding,CookieContainer cookieContainer1)
        {
            if (fileList == null)
            {
                return SendHttpRequestPost(url, keyValue, encoding);
            }

            url = UrlByVerification(url);
            encoding = EncodingByVerification(encoding);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            string boundary = "---------------------------" + Guid.NewGuid().ToString("N");

            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--"+boundary+"\r\n");

            request.ContentType = "multipart/form-data; boundary="+boundary;
            request.Method = "POST";
            request.CookieContainer = ClientCookieContainer;
            Stream stream = request.GetRequestStream();

            if (keyValue != null && keyValue.Count > 0)
            {
                string str = string.Empty;
                foreach (KeyValuePair<string,string> kvp in keyValue)
                {
                    stream.Write(boundaryBytes,0,boundaryBytes.Length);

                    str = string.Format(
                        "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}",
                        kvp.Key,kvp.Value);

                    byte[] data = encoding.GetBytes(str);
                    stream.Write(data,0,data.Length);
                }
            }

            string description = string.Empty;
            foreach (KeyValuePair<string,string> kvp in fileList)
            {
                stream.Write( boundaryBytes,0,boundaryBytes.Length);

                 description =string.Format(
                    "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n"  +
                    "Content-Type: application/octet-stream\r\n\r\n",
                    kvp.Key,Path.GetFileName(kvp.Value) );

                byte[] header = Encoding.UTF8.GetBytes(description);
                stream.Write(header,0,header.Length);

                byte[] body = File.ReadAllBytes(kvp.Value);
                stream.Write( body,0,body.Length);
            }

            boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            stream.Write(boundaryBytes,0,boundaryBytes.Length);

            stream.Close();

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        #endregion
		   
        #region 辅助方法
		/// <summary>
        /// URLs the by verification.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private string UrlByVerification(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Url为空");
            }
            return url;
        }

        private Encoding EncodingByVerification(Encoding encoding)
        {
            return encoding ?? (Encoding.UTF8);
        }

        #endregion
    }
}
