/*
[Discuz!] (C)2001-2009 Comsenz Inc.
This is NOT a freeware, use is subject to license terms

$Id: common.js 17535 2009-01-20 05:12:20Z monkey $
*/

var BROWSER = {};
var USERAGENT = navigator.userAgent.toLowerCase();
BROWSER.ie = window.ActiveXObject && USERAGENT.indexOf('msie') != -1 && USERAGENT.substr(USERAGENT.indexOf('msie') + 5, 3);
BROWSER.firefox = document.getBoxObjectFor && USERAGENT.indexOf('firefox') != -1 && USERAGENT.substr(USERAGENT.indexOf('firefox') + 8, 3);
BROWSER.chrome = window.MessageEvent && !document.getBoxObjectFor && USERAGENT.indexOf('chrome') != -1 && USERAGENT.substr(USERAGENT.indexOf('chrome') + 7, 10);
BROWSER.opera = window.opera && opera.version();
BROWSER.safari = window.openDatabase && USERAGENT.indexOf('safari') != -1 && USERAGENT.substr(USERAGENT.indexOf('safari') + 7, 8);
BROWSER.other = !BROWSER.ie && !BROWSER.firefox && !BROWSER.chrome && !BROWSER.opera && !BROWSER.safari;
BROWSER.firefox = BROWSER.chrome ? 1 : BROWSER.firefox;


var lang = new Array();
var userAgent = navigator.userAgent.toLowerCase();
var is_opera = userAgent.indexOf('opera') != -1 && opera.version();
var is_moz = (navigator.product == 'Gecko') && userAgent.substr(userAgent.indexOf('firefox') + 8, 3);
var is_ie = (userAgent.indexOf('msie') != -1 && !is_opera) && userAgent.substr(userAgent.indexOf('msie') + 5, 3);
var is_mac = userAgent.indexOf('mac') != -1;
var ajaxdebug = 0;
var codecount = '-1';
var codehtml = new Array();
var charset = 'utf-8';
//FixPrototypeForGecko
var cookiepath = typeof forumpath == 'undefined' ? '' : forumpath;
if (is_moz && window.HTMLElement) {
    HTMLElement.prototype.__defineSetter__('outerHTML', function(sHTML) {
        var r = this.ownerDocument.createRange();
        r.setStartBefore(this);
        var df = r.createContextualFragment(sHTML);
        this.parentNode.replaceChild(df, this);
        return sHTML;
    });

    HTMLElement.prototype.__defineGetter__('outerHTML', function() {
        var attr;
        var attrs = this.attributes;
        var str = '<' + this.tagName.toLowerCase();
        for (var i = 0; i < attrs.length; i++) {
            attr = attrs[i];
            if (attr.specified)
                str += ' ' + attr.name + '="' + attr.value + '"';
        }
        if (!this.canHaveChildren) {
            return str + '>';
        }
        return str + '>' + this.innerHTML + '</' + this.tagName.toLowerCase() + '>';
    });

    HTMLElement.prototype.__defineGetter__('canHaveChildren', function() {
        switch (this.tagName.toLowerCase()) {
            case 'area': case 'base': case 'basefont': case 'col': case 'frame': case 'hr': case 'img': case 'br': case 'input': case 'isindex': case 'link': case 'meta': case 'param':
                return false;
        }
        return true;
    });
    HTMLElement.prototype.click = function() {
        var evt = this.ownerDocument.createEvent('MouseEvents');
        evt.initMouseEvent('click', true, true, this.ownerDocument.defaultView, 1, 0, 0, 0, 0, false, false, false, false, 0, null);
        this.dispatchEvent(evt);
    }
}

/* Array.prototype.push = function(value) {
this[this.length] = value;
return this.length;
} */

function $(id) {
    return document.getElementById(id);
}
