//标签自动切换

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
