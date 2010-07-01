//<![CDATA[
var MiniSite = new Object();

MiniSite.Browser = {
    ie: /msie/.test(window.navigator.userAgent.toLowerCase()),
    moz: /gecko/.test(window.navigator.userAgent.toLowerCase()),
    opera: /opera/.test(window.navigator.userAgent.toLowerCase()),
    safari: /safari/.test(window.navigator.userAgent.toLowerCase())
};

MiniSite.$ = function(s) {
    return (typeof s == 'object') ? s : document.getElementById(s);
};

MiniSite.JsLoader = {
    load: function(sUrl, fCallback) {
        var _script = document.createElement('script');
        _script.setAttribute('charset', 'gb2312');
        _script.setAttribute('type', 'text/javascript');
        _script.setAttribute('src', sUrl);
        document.getElementsByTagName('head')[0].appendChild(_script);
        if (MiniSite.Browser.ie) {
            _script.onreadystatechange = function() {
                if (this.readyState == 'loaded' || this.readyStaate == 'complete') {
                    fCallback();
                }
            };
        } else if (MiniSite.Browser.moz) {
            _script.onload = function() {
                fCallback();
            };
        } else {
            fCallback();
        }
    }
};
MiniSite.Cookie = {
    set: function(name, value, expires, path, domain) {
        if (typeof expires == "undefined") {
            expires = new Date(new Date().getTime() + 24 * 3600 * 1000);
        }
        document.cookie = name + "=" + escape(value) + ((expires) ? "; expires=" + expires.toGMTString() : "") + ((path) ? "; path=" + path : "; path=/") + ((domain) ? ";domain=" + domain : "");
    },
    get: function(name) {
        var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
        if (arr != null) {
            return unescape(arr[2]);
        }
        return null;
    },
    clear: function(name, path, domain) {
        if (this.get(name)) {
            document.cookie = name + "=" + ((path) ? "; path=" + path : "; path=/") + ((domain) ? "; domain=" + domain : "") + ";expires=Fri, 02-Jan-1970 00:00:00 GMT";
        }
    }
};
MiniSite.Home = {
    defaultprovince: 4,
    timelapse: null,
    Provinc: { "北京市": 4, "上海市": 28, "天津市": 5, "重庆市": 19, "香港": 32, "澳门": 33, "台湾省": 31, "云南省": 20, "内蒙古": 9, "吉林省": 2, "四川省": 18, "宁夏": 14, "安徽省": 26, "山东省": 8, "山西省": 15, "广东省": 0, "广西": 22, "新疆": 10, "江苏省": 17, "江西省": 27, "河北省": 6, "河南省": 7, "浙江省": 29, "海南省": 23, "湖北省": 25, "湖南省": 24, "甘肃省": 12, "福建省": 30, "西藏": 11, "贵州省": 21, "辽宁省": 3, "陕西省": 16, "青海": 13, "黑龙江省": 1 },
    insertFlash: function(elm, url, w, h) {
        if (!document.getElementById(elm))
            return;
        var str = '';
        str += '<object width="' + w + '" height="' + h + '" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0">';
        str += '<param name="movie" value="' + url + '">';
        str += '<param name="wmode" value="transparent">';
        str += '<param name="quality" value="autohigh">';
        str += '<param name="allowScriptAccess" value="always" > ';
        str += '<embed width="' + w + '" height="' + h + '" src="' + url + '" quality="autohigh" wmode="transparent" type="application/x-shockwave-flash" plugspace="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"></embed>';
        str += '</object>';
        document.getElementById(elm).innerHTML = str;
    },
    _ip: "", /*省,市*/
    _print: function(province) {
        try { setdef(); } catch (e) { }
    },
    print: function() {
        var ok = function() {
            MiniSite.Home._print();
        }
        if (!MiniSite.Cookie.get("qq_index_ip_1hrcache")) {
            //MiniSite.Home.timelapse=setTimeout(ok,20000);
            MiniSite.JsLoader.load("http://fw.qq.com:80/ipaddress", function() {
                if (MiniSite.Home.timelapse != null) {
                    clearTimeout(MiniSite.Home.timelapse);
                };
                if (typeof IPData != "undefined") {
                    var expires = new Date(new Date().getTime() + 3600 * 1000);
                    MiniSite.Cookie.set('qq_index_ip_1hrcache', IPData[2] + ',' + IPData[3], expires);
                    MiniSite.Home._ip = IPData[2] + ',' + IPData[3];
                    MiniSite.Home._print(IPData[2]);
                } else {
                    //MiniSite.Home._print();
                }
            });
        } else {
            var ipAddress = MiniSite.Cookie.get("qq_index_ip_1hrcache");
            var ipAddressArr = ipAddress.split(",");
            return MiniSite.Home._print(ipAddressArr[0]);
        }
    }
};

window.onerror = function() {
    return true;
};

function loadJS(url, load) {
    var _script = document.createElement('script');
    _script.setAttribute('type', 'text/javascript');
    _script.setAttribute('src', url);
    document.getElementsByTagName('head')[0].appendChild(_script);
    if (!load) return;
    if (document.all) {
        _script.onreadystatechange = function() {
            if (this.readyState == 'loaded' || this.readyState == 'complete') {
                load();
            }
        };
    } else {
        _script.onload = function() {
            load();
        };
    }
}

function loadPng(o) {
    if (MiniSite.Browser.ie) {
        try {
            var img = o; var imgName = o.src.toUpperCase(); if (imgName.substring(imgName.length - 3, imgName.length) == "PNG") {
                var imgID = (img.id) ? "id='" + img.id + "' " : "";
                var imgClass = (img.className) ? "class='" + img.className + "' " : "";
                var imgTitle = (img.title) ? "title='" + img.title + "' " : "title='" + img.alt + "' ";
                var imgStyle = "display:inline-block;" + img.style.cssText;
                if (img.align == "left") imgStyle = "float:left;" + imgStyle;
                if (img.align == "right") imgStyle = "float:right;" + imgStyle;
                if (img.parentElement.href) imgStyle = "cursor:hand;" + imgStyle;
                var strNewHTML = "<span " + imgID + imgClass + imgTitle + " style=\"" + "width:" + img.width + "px; height:" + img.height + "px;" + imgStyle + ";" + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader" + "(src=\'" + img.src + "\', sizingMethod='image');\"></span>";
                img.outerHTML = strNewHTML;
            }
        } catch (e) { }
    }
}

function call_0410(type, item, page) {
    try {
        var purl = '';
        if (type == "A") {
            purl = window.event.srcElement.href;
        }
        else if (type == "IMG") {
            purl = window.event.srcElement.parentNode.href;
        }
        var a = document.cookie.match(new RegExp('(^|)o_cookie=([^;]*)(;|$)'));
        var iQQ = (a == null ? "" : unescape(a[2]));
        var iurl = 'http://btrace.qq.com/collect?sIp=&iQQ=' + iQQ + '&sBiz=qq.com&sOp=&iSta=&iTy=105&iFlow=&sItem=' + item + '&iPage=' + page + '&sUrl=' + escape(purl) + '&sLocalUrl=' + escape(location.href) + '&iRnd=' + Math.random();
        var img = new Image(1, 1); img.onerror = function() { };
        img.src = iurl;
    } catch (e) { }
}

function register0410(item, page) {
    try { var type = window.event.srcElement.tagName; if (type == "A" || type == "IMG") { call_0410(type, item, page); } return true; } catch (e) { }
}
        //]]>