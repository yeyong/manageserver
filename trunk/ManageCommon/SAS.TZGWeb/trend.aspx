<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="trend.aspx.cs" Inherits="trend" %>
<%@ Import Namespace="SAS.Entity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
<script src="js/jquery-exchange.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<div class="cot">
	<div class="trend mar_top_10" id="trend">
		<div id="trendnr">
			<div class="con"><a title="" href="#"><img alt="" src="images/ad/960x260.gif" /></a></div>
			<div class="con"><a title="" href="#"><img alt="" src="images/ad/960x260.gif" /></a></div>
			<div class="con"><a title="" href="#"><img alt="" src="images/ad/960x260.gif" /></a></div>
		</div>
		<ul id="trendt">
			<li></li>
			<li></li>
			<li></li>
		</ul>
	</div>
	<div class="trdtwo mar_top">
		<p class="trdtlt"><a title="" href="#"><img alt="" src="images/ad/313x275.gif" /></a></p>
		<ul class="trdtrt">
			<li><a title="" href="#"><img alt="" src="images/ad/199x137.gif" /></a></li>
			<li><a title="" href="#"><img alt="" src="images/ad/199x137.gif" /></a></li>
			<li><a title="" href="#"><img alt="" src="images/ad/199x137.gif" /></a></li>
			<li><a title="" href="#"><img alt="" src="images/ad/199x137.gif" /></a></li>
		</ul>
		<p class="trdtrt2"><a title="" href="#"><img alt="" src="images/ad/230x275.gif" /></a></p>
	</div>
	<div class="trdth mar_top" id="trdth">
		<div class="trdtht">
			<strong><img alt="潮我喜欢" src="images/index_tit2.gif" /></strong>
			<ul id="trdthrt">
			<%
                foreach (RecommendWithProduct rwpinfo in rwplist)
                {
            %>
				<li><%=rwpinfo.ctitle%></li>
			<%
                }
			%>
			</ul>
		</div>
		<div id="trdthrnr">
		    <%
                foreach (RecommendWithProduct rwpinfo in rwplist)
                {
            %>
			<div class="con">
				<ul class="trdcot">
				<%
                    int tkinfo__id = 1;
                    foreach (SAS.Entity.Domain.TaobaokeItem tkinfo in rwpinfo.item)
                    {
				%>
					<li class="trdcot1">
						<a title="<%=tkinfo.Title%>" href="productshow-<%=tkinfo.NumIid%>.html">
						<img alt="<%=tkinfo.Title%>" src="<%=tkinfo.PicUrl%>_160x160.jpg" />
						<span><%=tkinfo.Title%></span>
						</a>
						<em>已售出：<ins><%=tkinfo.CommissionNum%></ins>件</em>
						<b>￥<%=tkinfo.Price%></b>
					</li>
					<%if(tkinfo__id%5 > 0){%><li class="trdcot2"></li><%}%>
				<%
                    tkinfo__id++;
                    }
				%>					
				</ul>
			</div>
			<%
                }
			%>
		</div>
	</div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
<script type="text/javascript"> 

jQuery(document).ready(function(){
	jQuery("#trend").Exchange({ MIDS: "trendt", CIDS: "trendnr", count: 3, timer:5000, mousetype: 1 });
	jQuery("#trdth").Exchange({ MIDS: "trdthrt", CIDS: "trdthrnr", count: 6, timer:8000, mousetype: 1 });
});	
</script>
</asp:Content>

