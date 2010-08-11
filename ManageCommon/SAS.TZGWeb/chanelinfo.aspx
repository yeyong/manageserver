<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="chanelinfo.aspx.cs" Inherits="chanelinfo" %>
<%@ Import Namespace="SAS.Entity"%>
<%@ Import Namespace="SAS.Common"%>
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
            if (substr__length > 98) break;
            if (substr == "") continue;
            string[] str = substr.Split('|');
            if (str.Length < 2) continue;                    
            %>
			<a title="<%=str[1]%>" href="goodslist-<%=str[0]%>.html"><%=str[1]%></a> | 
			<%
                substr__length += Utils.GetStringLength(str[1]) + 3;
        }
            %>
            <a title="<%=subcinfo.Name%>" href="goodslist-p-<%=subcinfo.Cid%>.html">更多</a>
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
		<p class="chalad1"><%string[] indexad1 = adlist1.Split('|');if(indexad1.Length >=8){%><a target="_blank" title="<%=indexad1[5]%>" href="<%=indexad1[4]%>"><img alt="<%=indexad1[5]%>" src="<%=indexad1[1]%>" /></a><%}%></p>
		<ul class="chalad2">
			<li><%string[] indexad2 = adlist2.Split('|');if(indexad2.Length >=8){%><a target="_blank" title="<%=indexad2[5]%>" href="<%=indexad2[4]%>"><img alt="<%=indexad2[5]%>" src="<%=indexad2[1]%>" /></a><%}%></li>
			<li><%string[] indexad3 = adlist3.Split('|');if(indexad3.Length >=8){%><a target="_blank" title="<%=indexad3[5]%>" href="<%=indexad3[4]%>"><img alt="<%=indexad3[5]%>" src="<%=indexad3[1]%>" /></a><%}%></li>
			<li><%string[] indexad4 = adlist4.Split('|');if(indexad4.Length >=8){%><a target="_blank" title="<%=indexad4[5]%>" href="<%=indexad4[4]%>"><img alt="<%=indexad4[5]%>" src="<%=indexad4[1]%>" /></a><%}%></li>
			<li><%string[] indexad5 = adlist5.Split('|');if(indexad5.Length >=8){%><a target="_blank" title="<%=indexad5[5]%>" href="<%=indexad5[4]%>"><img alt="<%=indexad5[5]%>" src="<%=indexad5[1]%>" /></a><%}%></li>
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

