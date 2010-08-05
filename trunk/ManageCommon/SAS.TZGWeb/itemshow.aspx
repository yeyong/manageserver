<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="itemshow.aspx.cs" Inherits="itemshow" %>
<%@ Register TagPrefix="sas" TagName="viewgood" Src="~/usercontrol/viewgoods.ascx"%>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
<link href="css/jquery.cluetip.css" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
<script src="js/jquery.cluetip.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<%
    if (page_err == 0)
    {
%>
<div class="cot">
	<p class="site">您现在的位置：<a title="淘之购" href="index.html">淘之购</a> &gt; <%if(subcinfo!=null){%><a title="<%=rootinfo.Name%>" href="chanels_<%=rootinfo.Cid%>.html"><%=rootinfo.Name%></a> &gt; <a title="<%=subcinfo.Name%>" href="goodslist-p-<%=subcinfo.Cid%>.html"><%=subcinfo.Name%></a> &gt; <%}%><%=iteminfo.Title%></p>
	<div class="listlt">
		<div class="listlt1">
			<h3>相关类别</h3>
			<ul class="listlt1nr">
			<%
                if (subcinfo != null)
                {
                    foreach (string subcate in subcinfo.Cg_relateclass.Split(','))
                    {
                        if (subcate == "") continue;
                        string[] substr = subcate.Split('|');
                        if (substr.Length < 2) continue;    
		    %>
				<li><a title="<%=substr[0]%>" href="goodslist-<%=substr[0]%>.html"><%=substr[1]%></a></li>
			<%
                }
                }
			%>
			</ul>
		</div>
		<sas:viewgood runat="server" ID="viewgoods" />
	</div>
	<div class="listrt">
		<p class="showtu"><img alt="<%=iteminfo.Title%>" src="<%=iteminfo.PicUrl%>_310x310.jpg" /></p>
		<div class="showrt">
			<h1><%=iteminfo.Title%></h1>
			<p>价格：<em class="zi">￥<%=iteminfo.Price%></em></p>
			<p>已售出：<em class="f_f00">10<%=iteminfo.Volume%></em> 件</p>
			<p>
				<span>商品数：<%=iteminfo.Num%></span>
			</p>
			<em class="showline"></em>
			<p>掌柜店铺：<a title="<%=shopname%>" class="l_f18c08" href="<%=shopurl%>"><%=shopname%></a></p>
			<p>
				<span><i>累计信用：</i><b class="rankbg rank<%=System.Math.Ceiling((double)tkitem.SellerCreditScore / 5)%> rankw<%=tkitem.SellerCreditScore % 5==0?5:tkitem.SellerCreditScore % 5%>"></b></span>
				<span>好评率：<em class="f_f00"><%=shopscore%>%</em></span>
			</p>
			<p>所在地区：<%=shopaddress%></p>
			<p class="showan">
				<a title="<%=iteminfo.Title%>" href="<%=tkitem.ClickUrl%>"><img alt="点击查看详情" src="images/show_an1.gif" /></a>
				<a id="put" title="复制链接给好友" href="tooltip.html" rel="tooltip.html"><img alt="点击复制代码 分享给好友" src="images/show_an2.gif" /></a>
			</p>
		</div>
		<div class="showcon mar_top">
			<strong class="showcont">商品介绍</strong>
			<div class="showcon2"><%=iteminfo.Desc%></div>
		</div>
		<div class="shownr mar_top">
			<strong>商品所在店铺的其他商品</strong>
			<ul class="showcot">
			<%
    foreach (string shopgoodinfo in shopproducts.Split(','))
    {
        if (shopgoodinfo == "") continue;
        string[] subshopgood = shopgoodinfo.Split('|');
        if (subshopgood.Length < 4) continue;
			%>
				<li>
					<a title="<%=subshopgood[1]%>" href="productshow-<%=subshopgood[0]%>.html">
					<img alt="<%=subshopgood[1]%>" src="<%=subshopgood[3]%>_120x120.jpg" />
					<span><%=subshopgood[1]%></span>
					</a>
					<b>￥<%=subshopgood[2]%></b>
				</li>
			<%
    }
			%>
			</ul>
		</div>
		<div class="shownr mar_top">
			<strong>同类商品</strong>
			<ul class="showcot">
			<%
                foreach (SAS.Entity.Domain.TaobaokeItem tkiteminfo in sameclassproducts)
                {
			%>
				<li>
					<a title="<%=tkiteminfo.Title%>" href="productshow-<%=tkiteminfo.NumIid%>.html">
					<img alt="<%=tkiteminfo.Title%>" src="<%=tkiteminfo.PicUrl%>_120x120.jpg" />
					<span><%=tkiteminfo.Title%></span>
					</a>
					<b>￥<%=tkiteminfo.Price%></b>
				</li>
				<%
                }
				%>
			</ul>
		</div>
	</div>
</div>
<%
    }
    else
    {
%>
<!--#include file="msgbox.htm"-->
<%
    }
%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
<script type="text/javascript"> 
var nums = 5;
function closeCurrent(){
	if(nums < 0) nums = 5;
	jQuery('#nums').html(nums);
	jQuery('#copys').hide();
	jQuery('#result').show();
	if(nums > 0){			
		setTimeout("nums = nums - 1;closeCurrent();",1000);
	}else{
		nums = nums - 1;
		cluetip.style.display = 'none';
	}
}
function cancelCurrent(){
	cluetip.style.display = 'none';
}
jQuery(document).ready(function(){
	jQuery('#put').cluetip({ activation: 'click', sticky: true, width: 350, positionBy: 'bottomTop', closePosition: 'title',closeText: '<img src="images/cross.png" alt="close" />',cursor: 'pointer', dropShadow: false});
});	
</script>
</asp:Content>

