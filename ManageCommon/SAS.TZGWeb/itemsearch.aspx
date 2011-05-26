<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="itemsearch.aspx.cs" Inherits="itemsearch" %>
<%@ Import Namespace="SAS.Common" %>
<%@ Import Namespace="SAS.Entity" %>
<%@ Import Namespace="SAS.Entity.Domain" %>
<%@ Register TagPrefix="sas" TagName="viewgood" Src="~/usercontrol/viewgoods.ascx"%>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<div class="cot">
	<sas:viewgood runat="server" ID="viewgoods" />
	<div class="listrt mar_top" id="list" <%if(itemcount==0){%>style="display:none;"<%}%>>
		<strong class="listrtit">约有 <em class="f_f00"><%=itemcount%></em> 件商品符合“<em class="f_f00"><%=keyword%></em>”的查询结果</strong>
		<div class="listtit">
			<div class="listtnr">
				<p class="listtit1">
					<span class="listt<%=viewtype%2+1%>"><a title="<%=viewtype == 1 ? "切换到列表" : "切换到视图"%>" href="goodssearch-<%=pageid%>-<%=sortid%>-<%=viewtype==1?2:1%>-<%=startmoney%>-<%=endmoney%>-<%=searchkey%>.html"><%=viewtype == 1 ? "切换到列表" : "切换到视图"%></a></span>
					<span class="listt<%=sortid==1?4:3%>"><a title="销量从高到低" href="goodssearch-<%=pageid%>-1-<%=viewtype%>-<%=startmoney%>-<%=endmoney%>-<%=searchkey%>.html">销量</a></span>
					<span class="listt<%=sortid==2?4:3%>"><a title="信用从高到低" href="goodssearch-<%=pageid%>-2-<%=viewtype%>-<%=startmoney%>-<%=endmoney%>-<%=searchkey%>.html">信用</a></span>
					<span class="listt<%=sortid==3?6:5%>"><a title="价格从低到高" href="goodssearch-<%=pageid%>-3-<%=viewtype%>-<%=startmoney%>-<%=endmoney%>-<%=searchkey%>.html">价格</a></span>
					<span class="listt7">
					<form name="form1" onsubmit="return vaildform(this);">
						<input type="text" name="startmoney" class="input_jiaout" onblur="this.className='input_jiaout'" onfocus="this.className='input_jiaon'" style="width:30px;" value="<%=startmoney%>"/>
						-
						<input type="text" name="endmoney" class="input_jiaout" onblur="this.className='input_jiaout'" onfocus="this.className='input_jiaon'" style="width:30px;" value="<%=endmoney%>"/>
						<input type="submit" class="jia" value="提交" />
					</form>
					</span>
				</p>
				<p class="listtit2">
					<span>共<em class="f_f00"><%=itemcount%></em>条</span>
					<span><%=pageid%>/<%=pagecount%></span>
					<span class="listtit3"><a title="上一页" href="goodssearch-<%=prevpage%>-<%=sortid%>-<%=viewtype%>-<%=startmoney%>-<%=endmoney%>-<%=searchkey%>.html"><img alt="" src="images/arrow8.gif" /></a></span>
				<span class="listtit4"><a title="下一页" href="goodssearch-<%=nextpage%>-<%=sortid%>-<%=viewtype%>-<%=startmoney%>-<%=endmoney%>-<%=searchkey%>.html"><em>下一页</em><img alt="" src="images/arrow9.gif" /></a></span>
				</p>
			</div>
		</div>
		<ul class="<%=viewtype == 1 ? "listnr" : "listnr2"%>">
		<%
            foreach (TaobaokeItem tkinfo in itemlistitems)
            {
		%>
			<li>
				<a target="_blank" title="<%=Utils.RemoveHtml(tkinfo.Title)%>" href="productshow-<%=tkinfo.NumIid%>.html">
				<em class="listnrtu"><img alt="" src="<%=tkinfo.PicUrl%>_b.jpg" /></em>
				<em class="listnrt"><%=Utils.RemoveHtml(tkinfo.Title)%></em>
				</a>
				<p>30天内售出：<i class="zi"><%=tkinfo.Volume%></i>件</p>
				<strong>￥<%=tkinfo.Price%></strong>
				<b>所在地：<%=tkinfo.ItemLocation%><br/>掌柜：<a title="<%=tkinfo.Nick%>" href="<%=tkinfo.ShopClickUrl%>"><%=tkinfo.Nick%></a></b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank<%=System.Math.Ceiling((double)tkinfo.SellerCreditScore / 5)%> rankw<%=tkinfo.SellerCreditScore % 5==0?5:tkinfo.SellerCreditScore % 5%>"></i></ins>
			</li>
			<%
            }
            %>
		</ul>
		<p class="page f_666">
			<%=pagenumbers%>
		</p>
	</div>
	<div class="listrt" <%if(itemcount!=0){%>style="display:none;"<%}%>>
		<strong class="listrtit">约有 <em class="f_f00">0</em> 件商品符合“<em class="f_f00"><%=keyword%></em>”的查询结果</strong>
		<p class="seachlt"><img alt="" src="images/soso_tu1.gif" /></p>
		<p class="seachrt">
			<strong>很抱歉，没有找到与“<em class="f_f00"><%=keyword%></em>”相关的宝贝</strong>
			<span class="mar_top">建议您：</span>
			<span>1. 看看输入的文字是否有误</span>
			<span>2. 去掉可能不必要的字词，如“的”、“什么”等</span>
			<span>3. 调整关键字，如“兰蔻再生青春眼霜”改成“兰蔻 再生青春眼霜”或“兰蔻 眼霜”</span>
			<span class="mar_top">你是不是想找： </span>
			<span>
				<a class="l_f18c08" title="哈伦裤" href="goodssearch-s-哈伦裤.html">哈伦裤</a>
				<a class="l_f18c08" title="宽松" href="goodssearch-s-宽松T.html">宽松T</a>
				<a class="l_f18c08" title="淑女装" href="goodssearch-s-淑女装.html">淑女装</a>
				<a class="l_f18c08" title="手机" href="goodssearch-s-手机.html">手机</a>
				<a class="l_f18c08" title="男装" href="goodssearch-s-男装.html">男装</a>
				<a class="l_f18c08" title="女包" href="goodssearch-s-女包.html">女包</a>
				<a class="l_f18c08" title="雪纺" href="goodssearch-s-雪纺.html">雪纺</a>
				<a class="l_f18c08" title="头饰" href="goodssearch-s-头饰.html">头饰</a>
				<a class="l_f18c08" title="化妆品" href="goodssearch-s-化妆品.html">化妆品</a>
				<a class="l_f18c08" title="女鞋" href="goodssearch-s-女鞋.html">女鞋</a>
			</span>
		</p>
	</div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
<script type="text/javascript">
    function vaildform(formobj) {
        if (formobj.startmoney.value != "" && !isNumber(formobj.startmoney.value)) {
            alert("价格请输入整数！");
            return false;
        }
        if (formobj.endmoney.value != "" && !isNumber(formobj.endmoney.value)) {
            alert("价格请输入整数！");
            return false;
        }
        window.location.href = "goodssearch-<%=pageid%>-<%=sortid%>-<%=viewtype%>-" + formobj.startmoney.value + "-" + formobj.endmoney.value + "-<%=searchkey%>.html";
        return false;
    }
</script>
</asp:Content>

