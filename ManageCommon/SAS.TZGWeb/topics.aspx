<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="topics.aspx.cs" Inherits="topics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<div class="cot">
	<ul class="topic mar_top">
	    <%
            int tinfo__id = 1;   
            foreach (SAS.Entity.TaoBaoTopicInfo tinfo in tbtlist)
            {   
	    %>
		<li class="topic1">
			<a target="_blank" title="<%=tinfo.Title%>" href="topicshow-<%=tinfo.Tid%>.html">
				<img src="<%=tinfo.Pic%>" />
				<span><%=tinfo.Title%></span>
			</a>
		</li>
		<%
            if (tinfo__id % 2 > 0)
            {
         %>
		<li class="topic2"></li>
		<%
            }
         %>
		<%
                tinfo__id++;
            }
        %>
	</ul>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

