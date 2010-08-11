<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="brand.aspx.cs" Inherits="brand" %>
<%@ Import Namespace="SAS.Common"%>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<div class="cot">
    <%
        foreach (SAS.Entity.CategoryInfo cinfo in classlist)
        {
    %>
	<div class="xbrd mar_top">
		<strong><%=cinfo.Name%></strong>
		<p>
		    <%
                foreach (SAS.Entity.GoodsBrandInfo ginfo in SAS.Taobao.TaoBaos.GetGoodsBrandListByClass(cinfo.Cid))
                {
            %>
		    <a class="l_999" title="<%=ginfo.bname%>" href="goodssearch-s-<%=Utils.UrlEncode(ginfo.bname)%>.html"><%=ginfo.bname%>/<%=ginfo.spell%></a> | 
		    <%
                }
            %>
		</p>
		<ul class="xbrdnr">
		    <%
                int ginfo__id = 1;
                foreach (SAS.Entity.GoodsBrandInfo ginfo in SAS.Taobao.TaoBaos.GetGoodsBrandListByClass(cinfo.Cid))
                {
            %>
			<li class="xbrdnr1"><a title="<%=ginfo.bname%>" href="goodssearch-s-<%=Utils.UrlEncode(ginfo.bname)%>.html"><img alt="<%=ginfo.bname%>" src="<%=ginfo.logo%>" /></a></li>
			<%
                    if (ginfo__id % 11 > 0)
                    {
            %>
			<li class="xbrdnr2"></li>
			<%
                    }
                    ginfo__id++;
                }
            %>
		</ul>
	</div>
	<%
        }
    %>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

