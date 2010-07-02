jQuery.fn.ExtendClick = function(fromobj, toobj, tagname, indexnum) {
    var thefromobj = jQuery("#" + fromobj);
    var thetoobj = jQuery("#" + toobj);
    thefromobj.hide();
    jQuery(this).mouseover(function() {
        thefromobj.show();
    });
    jQuery(this).mouseout(function() {
        thefromobj.hide();
    });
    thefromobj.find(tagname).each(function(i) {
        if (i == indexnum) {
            thetoobj.html(jQuery(this).html());
        }
    });
};

///添加FLASH
jQuery.fn.addFlash = function(flashobj) {
    jQuery.extend(flashparm, flashobj);
    var s = "<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='" + flashparm.Codebase + "' id='" + flashparm.Flashid + "' width='" + flashparm.Width + "' height='" + flashparm.Height + "'>";
    s += " <param name='movie' value='" + flashparm.Movies + "'>";
    if (flashparm.Wmode && flashparm.Wmode != "")
        s += "<param name='wmode' value='" + flashparm.Wmode + "'>";
    s += "<param name='quality' value='autohigh'>";
    s += "<embed src='" + flashparm.Movies + "' quality='autohigh' id='" + flashparm.Flashid + "' width='" + flashparm.Width + "' height='" + flashparm.Height + "' wmode='" + flashparm.Wmode + "' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash'></embed>";
    s += "</object>";
    jQuery(this).append(s);
};

// JavaScript Document
var flashparm = {
    Codebase: "http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0",
    Flashid: "flash",
    Width: "100%",
    Height: "100%",
    Movies: "",
    Wmode: "transparent",
    Quality: "autohigh"
};

jQuery.fn.Couplet = function(parmsObj) {
    var parmarray = {
        closeicon: "images/cross.png",
        objsrc: "images/aa/aa2.jpg",
        objhref: "www.zheshangonline.com/",
        layout: "right",
        top: 100,
        distance: 100,
        width: 100,
        height: 100
    };

    parmsObj = parmsObj || {};
    jQuery.extend(parmarray, parmsObj);

    if (parmarray.layout == "left") {
        var right = document.documentElement.scrollWidth - parmarray.width - parmarray.distance;
    } else if (parmarray.layout == "right") {
        var right = parmarray.distance;
    }

    var objdiv = jQuery(this).floatdiv({ objsrc: parmarray.objsrc, objhref: parmarray.objhref, distance: parmarray.top, right: right });
    var objspan = jQuery("<span><img /></span>").css({ position: "absolute", right: 0 });
    var objfile = "";

    objspan.find("img").attr("src", parmarray.closeicon);
    objspan.find("img").click(function() { objdiv.hide(); });
    objspan.find("img").css({ "cursor": "pointer" });

    if ((parmarray.objsrc.indexOf(".jpg") != -1) || (parmarray.objsrc.indexOf(".gif") != -1) || (parmarray.objsrc.indexOf(".png") != -1)) {
        objdiv.find("a").append(jQuery("<img />").attr("src", parmarray.objsrc).css({ border: 0 }));
    } else if (parmarray.objsrc.indexOf(".swf") != -1) {
        objdiv.find("a").addFlash({
            Movies: parmarray.objsrc,
            Width: parmarray.width,
            Height: parmarray.height
        });
    }
    objdiv.append(objspan);
    if (jQuery.browser.msie && jQuery.browser.version < "7.0") {
        objdiv.attr("style", "top:expression(eval(document.documentElement.scrollTop + " + parmarray.top + "))");
        objdiv.css({ "position": "absolute", "right": right });
    }
    return jQuery(this);
};

///取得浮动层
jQuery.fn.floatdiv = function(locobj) {
    var locsty = {
        objsrc: "images/diaocha.gif",
        objhref: "www.zheshangonline.com/",
        position: "fixed",
        floattype: "right",
        right: 0,
        distance: 200, 	//和底部的距离
        pagewidth: 960,		//页面的有效宽度
        opentype:"_self"   //打开类型
    };

    locobj = locobj || {};
    jQuery.extend(locsty, locobj);
    var locdiv = jQuery("<div></div>");
    var objsrc = jQuery("<a></a>").attr("href", locsty.objhref);
    objsrc.attr("target", locsty.opentype);
    if (locsty.right == 0) locsty.right = (document.documentElement.scrollWidth - locsty.pagewidth) / 2 - 20 + "px";
    locdiv.css({
        top: locsty.distance,
        right: locsty.right,
        position: locsty.position,
        float: locsty.floattype
    });

    locdiv.append(objsrc);
    jQuery("body").append(locdiv);
    return locdiv;
}

///top方法
jQuery.fn.gettop = function(locobj) {
    var locsty = {
        objsrc: "images/diaocha.gif",
        objhref: "",
        position: "fixed",
        floattype: "right",
        distance: 150, 	//和底部的距离
        pagewidth: 960		//页面的有效宽度
    };
    locobj = locobj || {};
    jQuery.extend(locsty, locobj);

    var locdiv = jQuery(this).floatdiv(locobj);
    locdiv.find("a").append(jQuery("<img />").attr("src", locsty.objsrc).css({ border: 0 }));
    locdiv.append("<br/><a href='todayview.html'><img src='../images/view.gif'/></a>");

    window.onscroll = function() {
        if (document.documentElement.scrollTop == 0 && document.body.scrollTop == 0) {
            locdiv.hide("slow");
        } else {
            if (jQuery.browser.msie && jQuery.browser.version < "7.0") {
                locdiv.css("top", getPosition(locsty.distance + locdiv.find("img").height()));
                locdiv.css("position", "absolute");
            }
            else {
                locdiv.css("top", "");
                locdiv.css("bottom", locsty.distance);
            }
            locdiv.show("slow");
        }
    }
    locdiv.hide();
    return jQuery(this);
};

function getPosition(currObjH) {
    return document.documentElement.scrollTop + document.documentElement.clientHeight - currObjH + "px";
}