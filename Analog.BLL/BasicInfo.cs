using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Analog.BLL
{
    public class BasicInfo
    {
        /// <summary>
        /// 要请求的url
        /// </summary>
        private string _url;

        /// <summary>
        /// 请求的方式post或get
        /// </summary>
        private string _method;

        /// <summary>
        /// 要发送的数据
        /// </summary>
        private string _postData;

        /// <summary>
        /// 保存cookie的容器
        /// </summary>
        private CookieCollection _collection;
        public CookieCollection Collection
        {
            get { return _collection; }
        }

        /// <summary>
        /// 响应的状态true 成功,falsh失败
        /// </summary>
        private bool _status;
        public bool Status
        {
            get { return _status; }
        }

        /// <summary>
        /// 响应的页头
        /// </summary>
        private WebHeaderCollection _header;
        public WebHeaderCollection Header
        {
            get { return _header; }
        }

        /// <summary>
        /// 响应的页体
        /// </summary>
        private string _pageBody;
        public string PageBody
        {
            get { return _pageBody; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BasicInfo(string url, string method, string postdata, CookieCollection collection)
        {
            _url = url;
            _method = method;
            _postData = postdata;
            _collection = collection;
        }

        /// <summary>
        /// 请求页面的方法
        /// </summary>
        public void Request()
        {
            HttpWebRequest request;
            HttpWebResponse response;
            CookieContainer collection = new CookieContainer();
            if (_collection != null)
            {
                collection.Add(_collection);
            }

            request = (HttpWebRequest)HttpWebRequest.Create(_url);
            request.Method = _method;
            request.CookieContainer = collection;
            request.ContentType = "application/x-www-form-urlencoded";

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] b = encoding.GetBytes(_postData);
            request.ContentLength = b.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(b, 0, b.Length);
            }

            try
            {
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    _status = true;
                    _collection = response.Cookies;
                    _header = response.Headers;

                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default))
                    {
                        _pageBody = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                _status = false;
                //写日志
                //.....
                //结束
            }

        }
    }
}
