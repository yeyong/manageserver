using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using SAS.Taobao.Parser;
using SAS.Taobao.Request;
using SAS.Taobao.Util;

namespace SAS.Taobao
{
    /// <summary>
    /// 基于REST的TOP客户端。
    /// </summary>
    /// <example>
    /// ITopClient client = new TopRestClient("http://gw.sandbox.taobao.com/router/rest", "test", "test", "json");
    /// UserGetRequest request = new UserGetRequest();
    /// request.Fields = "user_id,nick,sex,created,location";
    /// request.Nick = "tbtest520";
    /// User user = client.Execute(request, new UserGetJsonParser());
    /// </example>
    public class NTWRestClient : INTWClient
    {
        public const string APP_KEY = "app_key";
        public const string FORMAT = "format";
        public const string METHOD = "method";
        public const string TIMESTAMP = "timestamp";
        public const string VERSION = "v";
        public const string SIGN = "sign";
        public const string PARTNER_ID = "partner_id";
        public const string SESSION = "session";
        public const string FORMAT_XML = "xml";

        private string serverUrl;
        private string appKey;
        private string appSecret;
        private long partnerId = 110L;
        private string format = FORMAT_XML;

        #region NTWRestClient Constructors

        public NTWRestClient(string serverUrl, string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverUrl = serverUrl;
        }

        public NTWRestClient(string serverUrl, string appKey, string appSecret, long partnerId)
            : this(serverUrl, appKey, appSecret)
        {
            this.partnerId = partnerId;
        }

        public NTWRestClient(string serverUrl, string appKey, string appSecret, string format)
            : this(serverUrl, appKey, appSecret)
        {
            this.format = format;
        }

        public NTWRestClient(string serverUrl, string appKey, string appSecret, long partnerId, string format)
            : this(serverUrl, appKey, appSecret, partnerId)
        {
            this.format = format;
        }

        #endregion

        #region INTWClient Members

        public T Execute<T>(INTWRequest request, INTWParser<T> parser)
        {
            return Execute<T>(request, parser, null);
        }

        public T Execute<T>(INTWRequest request, INTWParser<T> parser, string session)
        {
            // 添加协议级请求参数
            NTWDictionary txtParams = new NTWDictionary(request.GetParameters());
            txtParams.Add(METHOD, request.GetApiName());
            txtParams.Add(VERSION, "2.0");
            txtParams.Add(APP_KEY, appKey);
            txtParams.Add(FORMAT, format);
            txtParams.Add(PARTNER_ID, partnerId.ToString());
            txtParams.Add(TIMESTAMP, DateTime.Now);
            txtParams.Add(SESSION, session);

            // 添加签名参数
            txtParams.Add(SIGN, NTWUtil.SignTopRequest(txtParams, appSecret));

            // 是否需要上传文件
            string response;
            if (request is INTWUploadRequest)
            {
                INTWUploadRequest uploadRequest = (INTWUploadRequest)request;
                IDictionary<string, FileItem> fileParams = NTWUtil.CleanupDictionary(uploadRequest.GetFileParameters());
                response = WebUtils.DoPost(this.serverUrl, txtParams, fileParams);
            }
            else
            {
                response = WebUtils.DoPost(this.serverUrl, txtParams);
            }

            if (FORMAT_XML.Equals(format))
            {
                response = Regex.Replace(response, @"[\x00-\x08\x0b-\x0c\x0e-\x1f]", "");
            }
            TryParseException(response);
            return parser.Parse(response);
        }

        #endregion

        /// <summary>
        /// 尝试把错误响应转化为异常。
        /// </summary>
        /// <param name="response">API响应</param>
        private void TryParseException(string response)
        {
            if (FORMAT_XML.Equals(format))
            {
                // 为了避免二次解释XML，采用XPath访问节点
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(response);
                XmlNode errRsp = xmlDoc.SelectSingleNode("/error_response");
                if (errRsp != null)
                {
                    XmlNode errCodeNode = errRsp.SelectSingleNode("code");
                    XmlNode errMsgNode = errRsp.SelectSingleNode("msg");
                    XmlNode subErrCodeNode = errRsp.SelectSingleNode("sub_code");
                    XmlNode subErrMsgNode = errRsp.SelectSingleNode("sub_msg");

                    if (subErrCodeNode == null && subErrMsgNode == null)
                    {
                        throw new NTWException(errCodeNode.InnerText, errMsgNode.InnerText);
                    }
                    else
                    {
                        throw new NTWException(subErrCodeNode.InnerText, subErrMsgNode.InnerText);
                    }
                }
            }
            else
            {
                // 暂时只支持XML返回格式
                throw new NTWException("Unsupported response format!");
            }
        }
    }
}
