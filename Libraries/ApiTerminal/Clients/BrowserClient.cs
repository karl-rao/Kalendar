using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Kalendar.Zero.ApiTerminal.Clients
{
	/// <summary>
    /// 客户端请求类
    /// </summary>
    public class BrowserClient
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserClient"/> class.
        /// </summary>
        public BrowserClient()
        {
            ClientCookieContainer = new CookieContainer();
        }
        
        /// <summary>
        /// 模拟WebRequest
        /// </summary>
        /// <param name="url">目标地址</param>
        /// <param name="ssl">是否使用SSL协议</param>
        /// <param name="method">
        /// 请求方法
        /// GET, HEAD, POST, PUT, PATCH, DELETE, OPTIONS, TRACE
        /// </param>
        /// <param name="value">提交的数据</param>
        /// <param name="header">头部键值对</param>
        /// <param name="formData">表单键值对</param>
        /// <param name="uploadFiles">要上传的文件</param>
        /// <param name="encoding">
        /// 使用的编码
        /// UTF-8、GB2312、BIG5、、
        /// </param>
        /// <param name="accept">
        /// 接受数据格式
        /// text/xml、application/json、application/xml、application/x-www-form-urlencoded、、
        /// </param>
        /// <param name="contentType">提交数据格式</param>
        /// <param name="saveToFile">结果保存到文件</param>
        /// <returns>响应内容</returns>
	    public string SendHttpRequest(
	        string url,
	        bool ssl = false,
	        string method = "POST",
	        string value = "",
	        Dictionary<string, string> header = null,
	        Dictionary<string, string> formData = null,
	        Dictionary<string, string> uploadFiles = null,
	        string encoding = "utf-8",
	        string accept = "text/xml",
	        string contentType = "text/xml",
	        string saveToFile = "")
	    {
	        var result = "";
            try
            {
                url = UrlByVerification(url);
                var encoder = Encoding.GetEncoding(encoding);


                Logger.Debug("url=" + url);
                Logger.Debug("method=" + method);
                Logger.Debug("accept=" + accept);
                Logger.Debug("contentType=" + contentType);
                if (header != null && header.Count > 0)
                {
                    foreach (KeyValuePair<string, string> item in header)
                    {
                        Logger.Debug(item.Key+"="+ item.Value);
                    }
                }
                
                if (ssl)
                {
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    ServicePointManager.ServerCertificateValidationCallback =
                        new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                }

                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Accept = accept;
                request.Method =method;
                request.ContentType = contentType+ "; charset=" + encoder.WebName;
                request.CookieContainer = ClientCookieContainer;

                if (header != null && header.Count > 0)
                {
                    foreach (KeyValuePair<string, string> item in header)
                    {
                        request.Headers.Add(item.Key, item.Value);
                    }
                }

                Stream stream = null;
                if (value != "")
                {
                    stream = request.GetRequestStream();
                    var buffer = encoder.GetBytes(value);
                    stream.Write(buffer, 0, buffer.Length);
                }

                string boundary = "---------------------------" + Guid.NewGuid().ToString("N");
                byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                if (formData != null && formData.Any())
                {
                    request.ContentType = "multipart/form-data; boundary=" + boundary;

                    string str;
                    foreach (KeyValuePair<string, string> kvp in formData)
                    {
                        stream.Write(boundaryBytes, 0, boundaryBytes.Length);

                        str = string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}", kvp.Key,
                            kvp.Value);

                        byte[] data = encoder.GetBytes(str);
                        stream.Write(data, 0, data.Length);
                    }
                }

                if (uploadFiles != null && uploadFiles.Any())
                {
                    request.ContentType = "multipart/form-data; boundary=" + boundary;

                    if(stream==null)
                        stream = request.GetRequestStream();

                    string description;
                    foreach (KeyValuePair<string, string> kvp in uploadFiles)
                    {
                        stream.Write(boundaryBytes, 0, boundaryBytes.Length);
                        description =
                            $"Content-Disposition: form-data; name=\"{kvp.Key}\"; filename=\"{Path.GetFileName(kvp.Value)}\"\r\n" +
                            "Content-Type: application/octet-stream\r\n\r\n";

                        byte[] nameBytes = Encoding.UTF8.GetBytes(description);
                        stream.Write(nameBytes, 0, nameBytes.Length);

                        byte[] body = File.ReadAllBytes(kvp.Value);
                        stream.Write(body, 0, body.Length);
                    }
                }

                stream?.Close();

                using (WebResponse response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream(), encoder))
                    {
                        result = reader.ReadToEnd();
                    }
                }

                if (!string.IsNullOrEmpty(saveToFile))
                {
                    var sw = new StreamWriter(saveToFile);
                    sw.Write(result);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return result;
	    }

        /// <summary>
        /// WebClient 下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
	    public bool DownloadFile(string url, string filePath)
	    {
	        var result = true;
	        try
	        {
	            var client = new WebClient();
                client.DownloadFile(url,filePath);
	        }
	        catch (Exception ex)
	        {
	            Logger.Error(ex);
	            result = false;
	        }
	        return result;

	    }
        
        #region 辅助方法

        /// <summary>
        /// Gets or sets the client cookie container.
        /// </summary>
        /// <value>
        /// The client cookie container.
        /// </value>
        public CookieContainer ClientCookieContainer { get; set; }

        public bool CheckValidationResult(object sender,
                                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                System.Security.Cryptography.X509Certificates.X509Chain chain,
                                System.Net.Security.SslPolicyErrors errors)
        {
            //直接确认，不然打不开  
            return true;
        }

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
