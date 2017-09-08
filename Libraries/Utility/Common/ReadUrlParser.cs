using System;
using System.IO;
using System.Net;
using System.Text;

namespace Kalendar.Utility.Common
{

    /// <summary>
    /// 远程读取处理类
    /// </summary>
    public class ReadUrlParser
    {
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 读取与保存的字符集
        /// gb2312/utf-8/...
        /// </summary>
        /// <value>The char set.</value>
        public string CharSet { get; set; }

        private string _url;
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [status OK].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [status OK]; otherwise, <c>false</c>.
        /// </value>
        public bool StatusOK { get; set; }

        private string _httpcontent;
        /// <summary>
        /// Gets or sets the content of the HTTP.
        /// </summary>
        /// <value>
        /// The content of the HTTP.
        /// </value>
        public string HttpContent
        {
            get { return _httpcontent; }
            set { _httpcontent = value; }
        }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }

        /// <summary>
        /// 实例化一个远程地址
        /// </summary>
        /// <param name="uri">远程地址</param>
        public ReadUrlParser(string uri)
        {
            _url = uri;
            CharSet = "utf-8";//ConfigurationManager.AppSettings["FileEncoding"].ToString();
            if (_url != "")
            {
                try
                {
                    var myRequest = (HttpWebRequest)(WebRequest.Create(_url));
                    myRequest.AllowAutoRedirect = true;
                    myRequest.MaximumAutomaticRedirections = 3;
                    myRequest.UserAgent = "Mozilla/6.0 (MSIE 6.0; Windows NT 5.1;)";
                    myRequest.KeepAlive = true;
                    myRequest.Timeout = 120000;
                    _response = (HttpWebResponse)(myRequest.GetResponse());
                    if ((_response.StatusCode != HttpStatusCode.OK))
                    {
                        StatusOK = false;
                        ContentType = "";
                        _httpcontent = "";
                    }
                    else
                    {
                        StatusOK = true;
                        ContentType = _response.ContentType;
                        _httpcontent = Encode(_response);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    StatusOK = false;
                    ContentType = "";
                    _httpcontent = "";
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadUrlParser"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="charset">The charset.</param>
        public ReadUrlParser(string uri, string charset)
        {
            _url = uri;
            CharSet = charset;

            if (_url != "")
            {
                try
                {
                    var myRequest = (HttpWebRequest)(WebRequest.Create(_url));
                    myRequest.AllowAutoRedirect = true;
                    myRequest.MaximumAutomaticRedirections = 3;
                    myRequest.UserAgent = "Mozilla/6.0 (MSIE 6.0; Windows NT 5.1;)";
                    myRequest.KeepAlive = true;
                    myRequest.Timeout = 120000;
                    _response = (HttpWebResponse)(myRequest.GetResponse());
                    if ((_response.StatusCode != HttpStatusCode.OK))
                    {
                        StatusOK = false;
                        ContentType = "";
                        _httpcontent = "";
                    }
                    else
                    {
                        StatusOK = true;
                        ContentType = _response.ContentType;
                        _httpcontent = Encode(_response);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    StatusOK = false;
                    ContentType = "";
                    _httpcontent = "";
                }
            }
        }

        private HttpWebResponse _response;
        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public HttpWebResponse Response
        {
            get { return _response; }
            set { _response = value; }
        }

        /// <summary>
        /// 按照编码获取远程文件文本内容
        /// </summary>
        /// <param name="inputWebResponse">The input web response.</param>
        /// <returns></returns>
        private string Encode(WebResponse inputWebResponse)
        {
            string result;
            if (!(inputWebResponse == null))
            {
                Encoding htmlEncoding = Encoding.GetEncoding(CharSet);
                var stream = new StreamReader(inputWebResponse.GetResponseStream(), htmlEncoding);
                result = stream.ReadToEnd();
                stream.Close();
            }
            else
            {
                result = "";
            }
            return result;
        }

        /// <summary>
        /// 保存文件到服务器
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public bool Write(string filePath)
        {
            if (StatusOK)
            {
                try
                {
                    Encoding htmlEncoding = Encoding.GetEncoding(CharSet);
                    var stream = new StreamWriter(filePath, false, htmlEncoding);
                    stream.Write(_httpcontent);
                    stream.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return false;
                }
            }
            return false;
        }
    }
}
