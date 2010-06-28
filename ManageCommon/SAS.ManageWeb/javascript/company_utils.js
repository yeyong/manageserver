String.format = function() {
    if (arguments.length == 0) {
        return null;
    }
    var str = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
}

function isUndefined(variable) {
    return typeof variable == 'undefined' ? true : false;
}

function trim(str) {
    return (str + '').replace(/(\s+)$/g, '').replace(/^\s+/g, '');
}

function search(theform) {
    var svalue = trim(theform.searchcontent.value);
    var stype = 0;
    var stype2 = 0;
    if (!isUndefined(theform.searchtype)) stype = theform.searchtype.value;
    if (!isUndefined(theform.searchclass)) stype2 = theform.searchclass.value;
    if (svalue == "") return false;
    window.location = "zshy-" + stype2 + "-0-0-0-" + stype + "-0-0-" + encodeURIComponent(svalue).replace("'","%27") + ".html";
    return false;
}

function getsearchvalue(thevalue) {
    var svalue = trim(thevalue);
    svalue = svalue.replace("'", "%27");
    return encodeURIComponent(svalue);
}

var promotion_farmat1 = '&lt;a title="{0}" href="{1}"&gt;{0}&lt;/a&gt;';
var promotion_farmat2 = '[url={1}]{0}[/url]';
var promotion_0 = '';
var promotion_1 = '';
var promotion_2 = '';
var promotion_3 = '';

copyToClipboard = function(txt) {
    if (window.clipboardData) {
        window.clipboardData.clearData();
        window.clipboardData.setData("Text", txt);
    } else if (navigator.userAgent.indexOf("Opera") != -1) {
        window.location = txt;
    } else if (window.netscape) {
        try {
            netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
        } catch (e) {
            alert("您的firefox安全限制限制您进行剪贴板操作，请打开'about:config'将signed.applets.codebase_principal_support'设置为true'之后重试");
            return false;
        }
        var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
        if (!clip)
            return;
        var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
        if (!trans)
            return;
        trans.addDataFlavor('text/unicode');
        var str = new Object();
        var len = new Object();
        var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
        var copytext = txt;
        str.data = copytext;
        trans.setTransferData("text/unicode", str, copytext.length * 2);
        var clipid = Components.interfaces.nsIClipboard;
        if (!clip)
            return false;
        clip.setData(trans, null, clipid.kGlobalClipboard);
    }
    alert("地址复制成功，请通过各类网站平台发布您的企业推广信息，预祝您推广成功！");
    return true;
}