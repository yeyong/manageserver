(function($){
$.fn.extend({
        Scroll:function(opt,callback){
                //参数初始化
                if(!opt) var opt={};
                var _btnLeft = $("#"+ opt.left);//Shawphy:向上按钮
                var _btnRight = $("#"+ opt.right);//Shawphy:向下按钮
                var timerID;
                var _this=this.eq(0).find("ul:first");
                var     lineW=_this.find("li:first").width(), //获取行高
                        line=opt.line?parseInt(opt.line,10):parseInt(this.width()/lineW,10), //每次滚动的行数，默认为一屏，即父容器高度
                        speed=opt.speed?parseInt(opt.speed,10):500; //卷动速度，数值越大，速度越慢（毫秒）
                        timer=opt.timer //?parseInt(opt.timer,10):3000; //滚动的时间间隔（毫秒）
                if(line==0) line=1;
                var leftWidth=0-line*lineW;
                //滚动函数
                var scrollLeft=function(){
                        _btnLeft.unbind("click",scrollLeft); //Shawphy:取消向上按钮的函数绑定
                        _this.animate({
                                marginLeft:leftWidth
                        },speed,function(){
                                for(i=1;i<=line;i++){
                                        _this.find("li:first").appendTo(_this);
                                }
                                _this.css({marginLeft:0});
                                _btnLeft.bind("click",scrollLeft); //Shawphy:绑定向上按钮的点击事件
                        });

                }
                //Shawphy:向下翻页函数
                var scrollRight=function(){
                        _btnRight.unbind("click",scrollRight);
                        for(i=1;i<=line;i++){
                                _this.find("li:last").show().prependTo(_this);
                        }
                        _this.css({marginLeft:leftWidth});
                        _this.animate({
                                marginLeft:0
                        },speed,function(){
                                _btnRight.bind("click",scrollRight);
                        });
                }
               //Shawphy:自动播放
                var autoPlay = function(){
                        if(timer)timerID = window.setInterval(scrollLeft,timer);
                };
                var autoStop = function(){
                        if(timer)window.clearInterval(timerID);
                };
                 //鼠标事件绑定
                _this.hover(autoStop,autoPlay).mouseout();
                _btnLeft.css("cursor","pointer").click( scrollLeft ).hover(autoStop,autoPlay);//Shawphy:向上向下鼠标事件绑定
                _btnRight.css("cursor","pointer").click( scrollRight ).hover(autoStop,autoPlay);

        }

})

//水平垂直方向滚动


    $.fn.jMarquee = function(o) {
    o = $.extend({
    speed:70,
    step:1,//滚动步长
    direction:"up",//滚动方向
    visible:1//可见元素数量
    }, o || {});
    //获取滚动内容内各元素相关信息
    var i=0;
    var div=$(this);
    var ul=$("ul",div);
    var tli=$("li",ul);
    var liSize=tli.size();
    if(o.direction=="left")
        tli.css("float","left");
    var liWidth=tli.innerWidth();
    var liHeight=tli.height();
    var ulHeight=liHeight*liSize;
    var ulWidth=liWidth*liSize;
  
    //如果对象元素个数大于指定的显示元素则进行滚动，否则不滚动。
    if(liSize>o.visible){
        ul.append(tli.slice(0,o.visible).clone())  //复制前o.visible个li，并添加到ul的最后
        li=$("li",ul);
        liSize=li.size();
        
          //给滚动内容添加相关CSS样式
        //div.css({"position":"relative",overflow:"hidden"});
        //ul.css({"position":"relative",margin:"0",padding:"0","list-style":"none"});
        //li.css({margin:"0",padding:"0","position":"relative"});
        
        switch(o.direction){
            case "left":
                div.css("width",(liWidth*o.visible)+"px");
                ul.css("width",(liWidth*liSize)+"px");
                li.css("float","left");
                break;
            case "up":
                div.css({"height":(liHeight*o.visible)+"px"});
                ul.css("height",(liHeight*liSize)+"px");
                break;
        }
        
       
        var MyMar=setInterval(ylMarquee,o.speed);
        ul.hover(
            function(){clearInterval(MyMar);},
            function(){MyMar=setInterval(ylMarquee,o.speed);}
        );
    };
    function ylMarquee(){
         
        if(o.direction=="left"){
            if(div.scrollLeft()>=ulWidth){
                div.scrollLeft(0);
            }
            else
            {
                var leftNum=div.scrollLeft();
                leftNum+=parseInt(o.step);
                div.scrollLeft(leftNum)
            }
        }
        
        if(o.direction=="up"){
            if(div.scrollTop()>=ulHeight){
               div.scrollTop(0);
                
            }
            else{
               var topNum=div.scrollTop();
               topNum+=parseInt(o.step);
               div.scrollTop(topNum);
            }
        }
        
    };
   
}; 





})(jQuery);


