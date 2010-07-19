jQuery.fn.menudrop = function(defvalue){
	var thevalue = 0;
	thevalue = defvalue||0;
	if(thevalue>0){
		jQuery(this).find("div").removeClass();
		jQuery(this).find("div").addClass("menu2");
	}
	jQuery(this).find("div").hover(function() {
	jQuery(this).removeClass();
	jQuery(this).addClass("menu2");
	jQuery(this).find("li").hover(
		function(){
			jQuery(this).find("p").show();
			jQuery(this).addClass("nrli2");
		},
		function(){
			jQuery(this).find("p").hide();
			jQuery(this).removeClass().addClass("nrli1");
	});
},function(){if (thevalue != 1){jQuery(this).removeClass();jQuery(this).addClass("menu1");}});
};

//鼠标滑动变样式
(function (jQuery){
	this.objid = "crednr1";
	this.slides = function(billObj){
		var slide = {
			objid:"",
			cssout:"credcot1",
			csson:"credcot2"
		};
		var lbjdiv = jQuery("#" + this.objid);
		lbjdiv.find("li").mouseover(function(){
			lbjdiv.find("li").removeClass().addClass(slide.cssout);
			jQuery(this).removeClass().addClass(slide.csson);
		});
		
	};
	jQuery.slide = this;
	return jQuery;
})(jQuery);


//图片左右滚动
function MYScrollPic(myidname,framewidth,pagewidth,speed,space,play,time)
{	
	<!--//--><![CDATA[//><!--
	var scrollPic_02 = new ScrollPic();
	scrollPic_02.scrollContId   = myidname; //ID
	scrollPic_02.arrLeftId      = "LeftArr";//left按钮
	scrollPic_02.arrRightId     = "RightArr"; //right按钮

	scrollPic_02.frameWidth     = framewidth;//滚动处宽度
	scrollPic_02.pageWidth      = pagewidth; //滚动宽度

	scrollPic_02.speed          = speed; //滚动速度
	scrollPic_02.space          = space; //滚动范围
	scrollPic_02.autoPlay       = play; //自动滚动
	scrollPic_02.autoPlayTime   = time; //自动滚动时间

	scrollPic_02.initialize(); //初始化
							
	//--><!]]>
}




