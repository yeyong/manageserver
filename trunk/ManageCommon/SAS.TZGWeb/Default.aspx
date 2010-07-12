<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/main.master"%>
<asp:Content ID="thehead" ContentPlaceHolderID="head" runat="server">
<title><%=pagetitle%></title>
</asp:Content>
<asp:Content ID="themainbody" ContentPlaceHolderID="mainbody" runat="server">
<%
    foreach (SAS.Entity.Domain.ItemCat itemcatinfo in taoitemcatlist)
    {
        
     %>
     <%=itemcatinfo.Name%>||||||||<%=itemcatinfo.Cid%>||||||||<%=itemcatinfo.ParentCid%>||||<%=itemcatinfo.IsParent%>||||||||<%=itemcatinfo.SortOrder %><br />
     <%
    }
          %><%=SAS.Taobao.TaoBaos.GetItemCatCount(21)%>
</asp:Content>
