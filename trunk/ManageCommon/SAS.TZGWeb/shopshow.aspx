<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="shopshow.aspx.cs" Inherits="shopshow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<div class="cot">
	<p class="site">您现在的位置：<a title="淘之购" href="index.html">淘之购</a> &gt; <a title="信誉铺" href="credit.html">信誉铺</a> &gt; <%=sinfo.title%></p>
	<div class="shopsw">
		<h2><%=sinfo.title%></h2>
		<p class="shopswtu mar_top">
			<img alt="<%=sinfo.title%>" src="<%=shoppic_path + sinfo.pic_path%>" />
			<span><a target="_blank" title="<%=sinfo.title%>" href="<%=sinfo.click_url%>"><img alt="访问该店铺" src="images/show_an3.gif" /></a></span>
		</p>
		<p class="shopswzi mar_top">
			<b>
				<em>掌柜：</em><em class="f_fe8f00"><%=sinfo.nick%></em>
				<span><em>商品描述评分：</em><ins class="star star<%=Resore(sinfo.item_score)%>"><%=sinfo.item_score%></ins></span>
			</b>
			<b>
				<em>掌柜信用度：</em><em class="f_f00"><%=sinfo.shop_score%></em><strong class="rankbg rank<%=System.Math.Ceiling((double)sinfo.shop_level / 5)%> rankw<%=sinfo.shop_level % 5==0?5:sinfo.shop_level % 5%>"></strong>
				<span><em>服务态度评分：</em><ins class="star star<%=Resore(sinfo.service_score)%>"><%=sinfo.service_score%></ins></span>
			</b>
			<b>
				<em>好评率：</em><em class="f_f00">99.5%</em>
				<span><em>发货速度评分：</em><ins class="star star<%=Resore(sinfo.delivery_score)%>"><%=sinfo.delivery_score%></ins></span>
			</b>
			<b><em>创建时间：</em><i><%=sinfo.created%></i></b>
			<b><em>掌柜地址：</em><i><%=sinfo.shop_province+sinfo.shop_city%></i></b>
		</p>
		<div class="shopcard mar_top"></div>
	</div>
	<div class="shownr2 mar_top">
		<strong>掌柜推荐</strong>
		<ul class="showcot2">
		<%
            foreach (string shopgoodinfo in sinfo.relategoods.Split(','))
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
	<div class="shopcot mar_top">
		<strong class="shopcont">店铺介绍</strong>
		<div class="shopcon"><%=sinfo.shop_desc%></div>
	</div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

