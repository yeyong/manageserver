<%@ Page Title="" Language="C#" MasterPageFile="~/topic.master" CodeFile="activetyshow.aspx.cs" Inherits="activetyshow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<%if(page_err==0){%>
<style type="text/css">
    <%=ainfo.Stylecode%>
</style>
<%}%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<%
    if (page_err == 0)
    {
%>
<%=ainfo.Desccode%>
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
<%if(page_err==0){%><%=ainfo.Scriptcode%><%}%>
</script>
</asp:Content>

