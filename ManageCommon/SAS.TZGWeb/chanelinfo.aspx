<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="chanelinfo.aspx.cs" Inherits="chanelinfo" %>
<%@ Import Namespace="SAS.Entity"%>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
<script src="js/jquery-exchange.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<%
    if (page_err == 0)
    {
%>
<div class="cot">
	<ul class="chal mar_top_10">
	<%
        foreach (CategoryInfo subcinfo in csubclasslist)
        {
    %>
		<li>
			<strong><%=subcinfo.Name%></strong>
			<p>
			<%
        int substr__length = 0;
        foreach (string substr in subcinfo.Cg_relateclass.Split(','))
        {
            if (substr__length > 36) break;
            if (substr == "") continue;
            string[] str = substr.Split('|');
            if (str.Length < 2) continue;                    
            %>
			<a title="" href="goodslist-<%=str[0]%>.html"><%=str[1]%></a> |
			<%
        substr__length += str[1].Length;
        }
            %>
			</p>
		</li>
	<%
        }
    %>
	</ul>
	<ul class="chalbar">
	<%
        foreach (GoodsBrandInfo cbrandinfo in cbrandlist)
        {
    %>
		<li class="barli1"><a title="<%=cbrandinfo.bname%>" href="showbrand-<%=cbrandinfo.id%>.html"><img alt="<%=cbrandinfo.bname%>" src="<%=cbrandinfo.logo%>" /></a></li>
	<%
        }
    %>
	</ul>
	<div class="chalad mar_top">
		<p class="chalad1"><a title="" href="list.shtml"><img alt="" src="images/ad/220x287.gif" /></a></p>
		<ul class="chalad2">
			<li><a title="" href="list.shtml"><img alt="" src="images/ad/196x268.gif" /></a></li>
			<li><a title="" href="list.shtml"><img alt="" src="images/ad/195x268.gif" /></a></li>
			<li><a title="" href="list.shtml"><img alt="" src="images/ad/177x268.gif" /></a></li>
			<li><a title="" href="list.shtml"><img alt="" src="images/ad/176x268.gif" /></a></li>
		</ul>
	</div>
	<div class="trdth mar_top" id="chalprd">
		<div class="trdtht">
			<strong><img alt="潮我喜欢" src="images/index_tit2.gif" /></strong>
			<ul id="chalprdrt">
			<%
                foreach (CategoryInfo subcinfo in csubclasslist)
                {
            %>
				<li><%=subcinfo.Name%></li>
			<%
                }
            %>
			</ul>
		</div>
		<div id="chalprdnr">
		    <%
                foreach (CategoryInfo subcinfo in csubclasslist)
                {
            %>
			<div class="con">
				<ul class="trdcot">
				    <%
                        int tkiteminfo__id = 1;
                        foreach (SAS.Entity.Domain.TaobaokeItem tkiteminfo in SAS.Taobao.TaoBaos.GetRecommendProduct(Convert.ToInt16(TaoChanel.Chanel), subcinfo.Cid))
                        {
                    %>
					<li class="trdcot1">
						<a title="<%=tkiteminfo.Title%>" href="productshow-<%=tkiteminfo.NumIid%>.html">
						<img alt="<%=tkiteminfo.Title%>" src="<%=tkiteminfo.PicUrl%>_120x120.jpg" />
						<span><%=tkiteminfo.Title%></span>
						</a>
						<em>已售出：<ins><%=tkiteminfo.CommissionNum%></ins>件</em>
						<b>￥<%=tkiteminfo.Price%></b>
					</li>
					<%if (tkiteminfo__id % 5 > 0){%><li class="trdcot2"></li><%}%>
					<%
                        tkiteminfo__id++;
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
    jQuery(document).ready(function() {
        jQuery("#chalprd").Exchange({ MIDS: "chalprdrt", CIDS: "chalprdnr", count: 7, mousetype: 1 });
    });
</script>
</asp:Content>

