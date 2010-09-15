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
        objhref: "www.cnzshy.com/",
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

    var objdiv = jQuery(this).floatdiv({ objsrc: parmarray.objsrc, objhref: parmarray.objhref, distance: parmarray.top, right: right, opentype: "_blank" });
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
        objhref: "www.cnzshy.com/",
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
    locdiv.append("<br/><a href='todayview.html' target='_blank'><img src='../images/view.gif'/></a>");

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
//////////////////////////////////////////////////////////////////////////////////////////////////////
//标签自动切换
//////////////////////////////////////////////////////////////////////////////////////////////////////
(function($) {
    $.fn.extend({
        Exchange: function(opt, callback) {
            //参数初始化
            if (!opt) var opt = {};
            //时间范围
            var timeSpan = opt.timer;
            //数量
            var counts = opt.count;
            var haveall = opt.haveall;
            var mousetype = opt.mousetype;
            var j = 0;
            var TAuto = 1; //自动切换开关
            //初始化第一个li和第一个div
            $("#" + opt.MIDS).find("li:first").addClass("current");
            $("#" + opt.CIDS).find("div:first").show();
            if (haveall == 1) {
                $("#" + opt.CIDS + " > div").show();
            }
            //li移入事件
            $("#" + opt.MIDS + " li").each(function(i) {
                if (mousetype == 1) {
                    $(this).mouseover(function() {
                        $(this).addClass("current").siblings().removeClass(); //li的其它所有同辈元素移除样式
                        $("#" + opt.CIDS + " > div").hide();
                        $("#" + opt.CIDS + " div.con:eq(" + i + ")").show();
                        if (haveall == 1 && i == 0) {
                            $("#" + opt.CIDS + " > div").show();
                        }
                        TAuto = 0;
                    })
                }
                else {
                    $(this).click(function() {
                        $(this).addClass("current").siblings().removeClass(); //li的其它所有同辈元素移除样式
                        $("#" + opt.CIDS + " > div").hide();
                        $("#" + opt.CIDS + " div.con:eq(" + i + ")").show();
                        if (haveall == 1 && i == 0) {
                            $("#" + opt.CIDS + " > div").show();
                        }
                        TAuto = 0;
                    })
                }
            })
            //div移入事件
            $("#" + opt.CIDS + " div").each(function() {
                if (mousetype == 1) {
                    $(this).mouseover(function() {
                        TAuto = 0;
                    })

                }
                else {
                    $(this).click(function() {
                        TAuto = 0;
                    })
                }

            })
            //li移出事件
            $("#" + opt.MIDS + " li").each(function() {
                $(this).mouseout(function() {
                    TAuto = 1;
                })

            })
            //div移出事件
            $("#" + opt.CIDS + " div").each(function() {
                $(this).mouseout(function() {
                    TAuto = 1;
                })

            })
            if (timeSpan > 0) {
                var t = setInterval(function() {
                    if (TAuto == 1) {
                        $("#" + opt.MIDS + " li:eq(" + j + ")").trigger("mouseover"); //在每一个匹配的元素上触发某类事件
                        TAuto = 1;
                    }
                    if (j < counts) {
                        j++;
                    }
                    else {
                        j = 0;
                    }
                }, timeSpan)
            }
        }
    })
})(jQuery);

(function($) {
    $.fn.capslide = function(options) {
        var opts = $.extend({}, $.fn.capslide.defaults, options);
        return this.each(function() {
            $this = $(this);
            var o = $.meta ? $.extend({}, opts, $this.data()) : opts;

            if (!o.showcaption) $this.find('.ic_caption').css('display', 'none');
            else $this.find('.ic_text').css('display', 'none');

            var _img = $this.find('img:first');
            var w = _img.css('width');
            var h = _img.css('height');
            $('.ic_caption', $this).css({ 'color': o.caption_color, 'background-color': o.caption_bgcolor, 'bottom': '0px', 'width': w });
            $('.overlay', $this).css('background-color', o.overlay_bgcolor);
            $this.css({ 'width': w, 'height': h, 'border': o.border });
            $this.hover(
				function() {
				    if ((navigator.appVersion).indexOf('MSIE 7.0') > 0)
				        $('.overlay', $(this)).show();
				    else
				        $('.overlay', $(this)).fadeIn();
				    if (!o.showcaption)
				        $(this).find('.ic_caption').slideDown(500);
				    else
				        $('.ic_text', $(this)).slideDown(500);
				},
				function() {
				    if ((navigator.appVersion).indexOf('MSIE 7.0') > 0)
				        $('.overlay', $(this)).hide();
				    else
				        $('.overlay', $(this)).fadeOut();
				    if (!o.showcaption)
				        $(this).find('.ic_caption').slideUp(200);
				    else
				        $('.ic_text', $(this)).slideUp(200);
				}
			);
        });
    };
    $.fn.capslide.defaults = {
        caption_color: 'white',
        caption_bgcolor: 'black',
        overlay_bgcolor: 'blue',
        border: '1px solid #fff',
        showcaption: true
    };
})(jQuery);

(function($) {
    $.fn.extend({
        Scroll: function(opt, callback) {
            //参数初始化
            if (!opt) var opt = {};
            var _btnLeft = $("#" + opt.left); //Shawphy:向上按钮
            var _btnRight = $("#" + opt.right); //Shawphy:向下按钮
            var timerID;
            var _this = this.eq(0).find("ul:first");
            var lineW = _this.find("li:first").width(), //获取行高
                        line = opt.line ? parseInt(opt.line, 10) : parseInt(this.width() / lineW, 10), //每次滚动的行数，默认为一屏，即父容器高度
                        speed = opt.speed ? parseInt(opt.speed, 10) : 500; //卷动速度，数值越大，速度越慢（毫秒）
            timer = opt.timer //?parseInt(opt.timer,10):3000; //滚动的时间间隔（毫秒）
            if (line == 0) line = 1;
            var leftWidth = 0 - line * lineW;
            //滚动函数
            var scrollLeft = function() {
                _btnLeft.unbind("click", scrollLeft); //Shawphy:取消向上按钮的函数绑定
                _this.animate({
                    marginLeft: leftWidth
                }, speed, function() {
                    for (i = 1; i <= line; i++) {
                        _this.find("li:first").appendTo(_this);
                    }
                    _this.css({ marginLeft: 0 });
                    _btnLeft.bind("click", scrollLeft); //Shawphy:绑定向上按钮的点击事件
                });

            }
            //Shawphy:向下翻页函数
            var scrollRight = function() {
                _btnRight.unbind("click", scrollRight);
                for (i = 1; i <= line; i++) {
                    _this.find("li:last").show().prependTo(_this);
                }
                _this.css({ marginLeft: leftWidth });
                _this.animate({
                    marginLeft: 0
                }, speed, function() {
                    _btnRight.bind("click", scrollRight);
                });
            }
            //Shawphy:自动播放
            var autoPlay = function() {
                if (timer) timerID = window.setInterval(scrollLeft, timer);
            };
            var autoStop = function() {
                if (timer) window.clearInterval(timerID);
            };
            //鼠标事件绑定
            _this.hover(autoStop, autoPlay).mouseout();
            _btnLeft.css("cursor", "pointer").click(scrollLeft).hover(autoStop, autoPlay); //Shawphy:向上向下鼠标事件绑定
            _btnRight.css("cursor", "pointer").click(scrollRight).hover(autoStop, autoPlay);

        }

    })

    //水平垂直方向滚动


    $.fn.jMarquee = function(o) {
        o = $.extend({
            speed: 70,
            step: 1, //滚动步长
            direction: "up", //滚动方向
            visible: 1//可见元素数量
        }, o || {});
        //获取滚动内容内各元素相关信息
        var i = 0;
        var div = $(this);
        var ul = $("ul", div);
        var tli = $("li", ul);
        var liSize = tli.size();
        if (o.direction == "left")
            tli.css("float", "left");
        var liWidth = tli.innerWidth();
        var liHeight = tli.height();
        var ulHeight = liHeight * liSize;
        var ulWidth = liWidth * liSize;

        //如果对象元素个数大于指定的显示元素则进行滚动，否则不滚动。
        if (liSize > o.visible) {
            ul.append(tli.slice(0, o.visible).clone())  //复制前o.visible个li，并添加到ul的最后
            li = $("li", ul);
            liSize = li.size();

            //给滚动内容添加相关CSS样式
            //div.css({"position":"relative",overflow:"hidden"});
            //ul.css({"position":"relative",margin:"0",padding:"0","list-style":"none"});
            //li.css({margin:"0",padding:"0","position":"relative"});

            switch (o.direction) {
                case "left":
                    div.css("width", (liWidth * o.visible) + "px");
                    ul.css("width", (liWidth * liSize) + "px");
                    li.css("float", "left");
                    break;
                case "up":
                    div.css({ "height": (liHeight * o.visible) + "px" });
                    ul.css("height", (liHeight * liSize) + "px");
                    break;
            }


            var MyMar = setInterval(ylMarquee, o.speed);
            ul.hover(
            function() { clearInterval(MyMar); },
            function() { MyMar = setInterval(ylMarquee, o.speed); }
        );
        };
        function ylMarquee() {
            if (o.direction == "left") {
                if (div.scrollLeft() >= ulWidth) {
                    div.scrollLeft(0);
                }
                else {
                    var leftNum = div.scrollLeft();
                    leftNum += parseInt(o.step);
                    div.scrollLeft(leftNum)
                }
            }
            if (o.direction == "up") {
                if (div.scrollTop() >= ulHeight) {
                    div.scrollTop(0);
                }
                else {
                    var topNum = div.scrollTop();
                    topNum += parseInt(o.step);
                    div.scrollTop(topNum);
                }
            }
        };
    };
})(jQuery);