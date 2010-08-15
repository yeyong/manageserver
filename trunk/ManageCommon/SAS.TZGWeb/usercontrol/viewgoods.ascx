<%@ Control Language="C#" CodeFile="viewgoods.ascx.cs" Inherits="usercontrol_viewgoods" %>
<div class="listlt2 mar_top">
    <strong>浏览过的商品</strong>
    <ul class="listlt2nr">
    <%
        foreach (string str in viewlist.Split(','))
        {
            if (str == "") continue;
            string[] substr = str.Split('|');
            if (substr.Length < 3) continue;
    %>
        <li><a target="_blank" title="<%=SAS.Common.Utils.UrlDecode(substr[1])%>" href="productshow-<%=substr[0]%>.html"><img alt="<%=SAS.Common.Utils.UrlDecode(substr[1])%>" src="<%=substr[3]%>_sum.jpg" /><span>￥<%=substr[2]%></span> </a></li>
    <%
        }
    %>
    </ul>
</div>
