<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="woman.aspx.cs" Inherits="woman" %>
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
		<div id="womnr">
		    <%
            int adinfo__id = 1;
            foreach (AdShowInfo adinfo in adlist1)
            {
                if (adinfo__id > 3) break;
                string[] astr = adinfo.Parameters.Split('|');
                if (astr.Length < 8) continue;
		    %>
			<div class="con"><a title="<%=astr[5]%>" href="<%=astr[4]%>"><img alt="<%=astr[5]%>" src="<%=astr[1]%>" /></a></div>
			<%
                adinfo__id++;
            }
            %>
		</div>
		<ul id="womt">
			<li></li>
			<li></li>
			<li></li>
		</ul>
	</div>
	<ul class="woman2 mar_top">
	    <%
            int adinfo2__id = 1;
            foreach (AdShowInfo adinfo2 in adlist2)
            {
                if (adinfo2__id > 6) break;
                string[] astr = adinfo2.Parameters.Split('|');
                if (astr.Length < 8) continue;
		%>
		<li><a title="<%=astr[5]%>" href="<%=astr[4]%>"><img alt="<%=astr[5]%>" src="<%=astr[1]%>" /></a></li>
		<%
            adinfo2__id++;
            }
		%>
	</ul>
	<div class="trdth mar_top" id="trdth">
		<div class="trdtht">
			<strong><img alt="女人志" src="images/woman_tit1.gif" /></strong>
			<ul id="womanrt">
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
	jQuery("#trend").Exchange({ MIDS: "womt", CIDS: "womnr", count: 3, timer:5000, mousetype: 1 });
	jQuery("#trdth").Exchange({ MIDS: "womanrt", CIDS: "trdthrnr", count: 6, timer:8000, mousetype: 1 });
});	
</script>
</asp:Content>

