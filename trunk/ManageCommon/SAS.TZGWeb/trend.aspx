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
		<ul id="trendt">
			<%for(int sumi = adinfo__id-1;sumi>0;sumi--){%><li></li><%}%>
		</ul>
	</div>
	<div class="trdtwo mar_top">
		<p class="trdtlt"><%string[] indexad2 = adlist2.Split('|');if(indexad2.Length >=8){%><a title="<%=indexad2[5]%>" href="<%=indexad2[4]%>"><img alt="<%=indexad2[5]%>" src="<%=indexad2[1]%>" /></a><%}%></p>
		<ul class="trdtrt">
		<%
            int adinfo3__id = 1;
            foreach (AdShowInfo adinfo3 in adlist3)
            {
                if (adinfo3__id > 4) break;
                string[] astr = adinfo3.Parameters.Split('|');
                if (astr.Length < 8) continue;
		    %>
			<li><a title="<%=astr[5]%>" href="<%=astr[4]%>"><img alt="<%=astr[5]%>" src="<%=astr[1]%>" /></a></li>
			<%
                adinfo3__id++;
            }
			%>
		</ul>
		<p class="trdtrt2"><%string[] indexad4 = adlist4.Split('|');if(indexad4.Length >=8){%><a title="<%=indexad4[5]%>" href="<%=indexad4[4]%>"><img alt="<%=indexad4[5]%>" src="<%=indexad4[1]%>" /></a><%}%></p>
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
						<em>30成交量：<ins><%=tkinfo.Volume%></ins>件</em>
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

