<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="index.aspx.cs" Inherits="index" %>
<%@ Import Namespace="SAS.Entity"%>
<asp:Content ID="Content0" ContentPlaceHolderID="styles" runat="server">
<link href="css/index.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" Runat="Server">
<script src="js/scrollPic.js" type="text/javascript"></script>
<script src="js/jquery-exchange.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainbody" Runat="Server">
<div class="cot">
	<div class="ban mar_top" id="banner">
		<p class="bantu"><a title="广告一" href="www.zheshangonline.com"><img alt="广告一" src="images/ad/380x260.gif" /></a></p>
		<ul class="banxtu">
			<li><img rel="www.wumeiwang.com" alt="广告二" src="images/ad/126x86.gif" /></li>
			<li><img rel="www.baidu.com" alt="广告三" src="images/ad/380x260.gif" /></li>
			<li><img rel="www.google.com" alt="广告四" src="images/ad/126x86.gif" /></li>
		</ul>
	</div>
	<div class="hpin mar_top">
		<strong>最新活动</strong>
		<ul class="hpinnr">
			<li>[<em>2010-07-22</em>] <a title="" href="shop_show.shtml">Michelle 美包专卖</a></li>
			<li>[<em>2010-07-20</em>] <a title="" href="shop_show.shtml">杜拉拉 时尚潮流 风向标</a></li>
			<li>[<em>2010-07-19</em>] <a title="" href="shop_show.shtml">NOKIE 数码专卖</a></li>
			<li>[<em>2010-07-18</em>] <a title="" href="shop_show.shtml">时尚潮流 日用专卖</a></li>
			<li>[<em>2010-07-17</em>] <a title="" href="shop_show.shtml">想吃就吃 不怕胖</a></li>
			<li>[<em>2010-07-17</em>] <a title="" href="shop_show.shtml">瘦身美体 Michelle</a></li>
		</ul>
		<p class="onead mar_top_5"><a title="" href="#"><img alt="" src="images/ad/223x95.gif" /></a></p>
	</div>
</div>
<div class="cot">
	<div class="pref mar_top">
		<strong><img alt="优惠活动场" src="images/index_tit1.gif" /></strong>
		<em><a title="" href="topics.html">查看更多专题&gt;&gt;</a></em>
		<div class="prefnr">
			<p><a title="" href="#"><img alt="" src="images/ad/156x243.gif" /></a></p>
			<p><a title="" href="#"><img alt="" src="images/ad/203x243.gif" /></a></p>
			<p><a title="" href="#"><img alt="" src="images/ad/192x243.gif" /></a></p>
			<ul class="prefzi">
			<%
                int indextopicinfo__id = 1;	    
                foreach (TaoBaoTopicInfo indextopicinfo in indextopiclist)
                {	    
		    %>
				<li><a title="<%=indextopicinfo.Title%>" href="topicshow-<%=indextopicinfo.Tid%>.html"><%=indextopicinfo.Title%></a></li>
				<%if (indextopicinfo__id % 3 == 0){%><li></li><%}%>
				<%
                    indextopicinfo__id++;
                }
                %>
			</ul>
		</div>
	</div>
	<div class="twoad" id="twoad">
		<div id="twoadnr">
			<div class="con"><a title="" href="#"><img alt="" src="images/ad/218x271.gif" /></a></div>
			<div class="con"><a title="" href="#"><img alt="" src="images/ad/220x287.gif" /></a></div>
			<div class="con"><a title="" href="#"><img alt="" src="images/ad/218x271.gif" /></a></div>
		</div>
		<ul id="twoadtit">
			<li></li>
			<li></li>
			<li></li>
		</ul>
	</div>
</div>
<div class="cot" id="like">
	<div class="liketit mar_top">
		<strong><img alt="潮我喜欢" src="images/index_tit2.gif" /></strong>
		<ul id="likert">
			<li>衣</li>
			<li>鞋</li>
			<li>饰</li>
			<li>妆</li>
			<li>居</li>
		</ul>
	</div>
	<div id="likenr">
		<div class="con">
			<p class="mar1"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<p class="mar2"><a title="" href="#"><img alt="" src="images/ad/175x340x2.gif" /></a></p>
			<p class="mar3"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<p class="mar4"><a title="" href="#"><img alt="" src="images/ad/175x340x2.gif" /></a></p>
			<p class="mar5"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<span></span>
		</div>
		<div class="con">
			<p class="mar1"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<p class="mar2"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<p class="mar3"><a title="" href="#"><img alt="" src="images/ad/175x340x2.gif" /></a></p>
			<p class="mar4"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<p class="mar5"><a title="" href="#"><img alt="" src="images/ad/175x340x2.gif" /></a></p>
			<span></span>
		</div>
		<div class="con">
			<p class="mar1"><a title="" href="#"><img alt="" src="images/ad/175x340x2.gif" /></a></p>
			<p class="mar2"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<p class="mar3"><a title="" href="#"><img alt="" src="images/ad/175x340x2.gif" /></a></p>
			<p class="mar4"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<p class="mar5"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<span></span>
		</div>
		<div class="con">
			<p class="mar1"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<p class="mar2"><a title="" href="#"><img alt="" src="images/ad/175x340x2.gif" /></a></p>
			<p class="mar3"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<p class="mar4"><a title="" href="#"><img alt="" src="images/ad/175x340x2.gif" /></a></p>
			<p class="mar5"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<span></span>
		</div>
		<div class="con">
			<p class="mar1"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<p class="mar2"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<p class="mar3"><a title="" href="#"><img alt="" src="images/ad/175x340x2.gif" /></a></p>
			<p class="mar4"><a title="" href="#"><img alt="" src="images/ad/175x340.gif" /></a></p>
			<p class="mar5"><a title="" href="#"><img alt="" src="images/ad/175x340x2.gif" /></a></p>
			<span></span>
		</div>
	</div>
</div>
<div class="cot">
	<ul class="inxbrd mar_top">
	<%
        foreach (GoodsBrandInfo ginfo in ginfolist)
        {
	%>
		<li><a title="<%=ginfo.bname%>" href="<%=ginfo.id%>"><img alt="<%=ginfo.bname%>" src="<%=ginfo.logo%>" /></a></li>
	<%
        }
    %>
	</ul>
</div>
<div class="cot">
	<div class="three">
		<p class="threead mar_top"><a title="" href="#"><img alt="" src="images/ad/220x287.gif" /></a></p>
		<div class="crown mar_top mar_left">
			<p class="crowntit">
				<strong><img alt="皇冠店铺" src="images/index_tit3.gif" /></strong>
				<em><a title="" href="credit.html">查看更多店铺&gt;&gt;</a></em>
			</p>
			<div class="crownr">
				<p class="crownlt" id="LeftArr"></p>
				<ul class="crowncot" id="crown">
				<%
                    int sinfo__id = 1;		    
                    foreach (ShopDetailInfo sinfo in shoplist)
                    {		    
			    %>
					<li>
						<a title="<%=sinfo.title%>" href="shop_show.shtml">
						<p><img alt="<%=sinfo.title%>" src="<%=shoppic_path + sinfo.pic_path%>" /></p>
						<span><%=sinfo.title%></span>
						</a>
						<strong><ins class="rankbg rank<%=System.Math.Ceiling((double)sinfo.shop_level / 5)%> rankw<%=sinfo.shop_level % 5==0?5:sinfo.shop_level % 5%>"></ins></strong>
						<em>好评率：<i class="f_f00"><%=decimal.Round(decimal.Parse((((double)sinfo.good_num / (double)sinfo.total_num)*100).ToString()),2)%>%</i></em>
						<em>所在地：<%=sinfo.shop_province+sinfo.shop_city%></em>
						<em>收藏数：<i class="f_f00">1258</i></em>
						<b><%=sinfo__id%></b>
					</li>
					<%if (sinfo__id % 4 > 0){%><li class="li2"></li><%}%>
				<%
                        sinfo__id++;
                    }
				%>
				</ul>
				<p class="crownrt" id="RightArr"></p>
			</div>
		</div>
	</div>
	<ul class="brand mar_top" id="brand">
		<li style="background:url(images/ad/218x70.gif) no-repeat; "><a title="" href="list.shtml"></a></li>
		<li style="background:url(images/ad/218x70.gif) no-repeat; "><a title="" href="list.shtml"></a></li>
		<li style="background:url(images/ad/218x70.gif) no-repeat; "><a title="" href="list.shtml"></a></li>
		<li style="background:url(images/ad/218x70.gif) no-repeat; "><a title="" href="list.shtml"></a></li>
		<li style="background:url(images/ad/218x70.gif) no-repeat; "><a title="" href="list.shtml"></a></li>
		<li style="background:url(images/ad/218x70.gif) no-repeat; "><a title="" href="list.shtml"></a></li>
		<li style="background:url(images/ad/218x70.gif) no-repeat; "><a title="" href="list.shtml"></a></li>
		<li style="background:url(images/ad/218x70.gif) no-repeat; "><a title="" href="list.shtml"></a></li>
	</ul>
</div>
<div class="cot">
	<p class="link mar_top">
		<strong>友 情链 接</strong>
		<span><a class="l_666" title="" href="#">浙商黄页</a> | <a class="l_666" title="" href="#">淘宝皇冠店</a> | <a class="l_666" title="" href="#">团购网站大全</a> | <a class="l_666" title="" href="#">浙商黄页</a> | <a class="l_666" title="" href="#">淘宝皇冠店</a> | <a class="l_666" title="" href="#">团购网站大全</a> | <a class="l_666" title="" href="#">浙商黄页</a> | <a class="l_666" title="" href="#">淘宝皇冠店</a> | <a class="l_666" title="" href="#">团购网站大全</a> | <a class="l_666" title="" href="#">浙商黄页</a> | <a class="l_666" title="" href="#">淘宝皇冠店</a> | <a class="l_666" title="" href="#">团购网站大全</a> | <a class="l_666" title="" href="#">浙商黄页</a> | <a class="l_666" title="" href="#">淘宝皇冠店</a> | <a class="l_666" title="" href="#">团购网站大全</a></span>
		<em></em>
	</p>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
<script type="text/javascript">
    MYScrollPic("crown", 460, 460, 10, 100, true, 6); //调用方法
    jQuery(document).ready(function() {
        jQuery("#twoad").Exchange({ MIDS: "twoadtit", CIDS: "twoadnr", count: 3, mousetype: 1 });
        jQuery("#like").Exchange({ MIDS: "likert", CIDS: "likenr", count: 5, timer: 8000, mousetype: 1 });
        var banobj = jQuery('#banner').find("p");
        jQuery('#banner').find("li").mouseover(function() {
            banobj.find("a").attr("href", jQuery(this).find("img").attr("rel"));
            banobj.find("a").attr("title", jQuery(this).find("img").attr("alt"));
            banobj.find("img").attr("alt", jQuery(this).find("img").attr("alt"));
            banobj.find("img").attr("src", jQuery(this).find("img").attr("src"));
        });
        jQuery('#brand').find("li:first").css("background-position", "bottom");
        jQuery('#brand').find("li").mousemove(function() {
            jQuery('#brand').find("li").css("background-position", "top");
            jQuery(this).css("background-position", "bottom");
        });
    });
</script>
</asp:Content>

