﻿using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SAS.Web.Services.API
{
    [XmlRoot("error_response", Namespace = "http://www.cnzshy.com/api/", IsNullable = false)]
    public class Error
    {
        [JsonProperty("error_code")]
        [XmlElement("error_code")]
        public int ErrorCode;

        [JsonProperty("error_msg")]
        [XmlElement("error_msg")]
        public string ErrorMsg;

        [JsonIgnore]
        [XmlElement("request_args", IsNullable = false)]
        public ArgResponse Args;

        [JsonProperty("request_args")]
        public Arg[] ArgArrary;

    }
}
