<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="topicshow.aspx.cs" Inherits="topicshow" %>
<%@ Register TagPrefix="tao" TagName="error" Src="~/toolcontrol/showmsg.ascx"%>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">

<%
    if (page_err == 0)
    {
%>
<div class="cot">
<iframe id="frame_content" style="float:left;margin-Left:0px;padding-left:0px;" src="<%=tinfo.Url%>" frameborder="0" name="naturemainFrame" width="<%=tinfo.Width%>" height="<%=tinfo.Height%>" scrolling="no"></iframe>
</div>
<%
    }
    else
    {
%>
<tao:error runat="server" ID="taoerror" />
<%=msgbox_text%>
<%
    }
%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

