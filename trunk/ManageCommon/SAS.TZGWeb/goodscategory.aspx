<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="goodscategory.aspx.cs" Inherits="goodscategory" %>
<%@ Import Namespace="SAS.Entity" %>
<%@ Import Namespace="SAS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainbody" Runat="Server">
<div class="cot">
    <%
        foreach (CategoryInfo pcinfo in parentcinfo)
        {
         %>
	<div class="caty mar_top">
		<h4><%=pcinfo.Name%></h4>
		<ul class="catynr">
		    <%
                foreach (CategoryInfo subcinfo in SAS.Taobao.TaoBaos.GetCategoryListByParentID(pcinfo.Cid))
                {
            %>
			<li>
				<strong><%=subcinfo.Name%></strong>				
				<p>
				<%
                    int subsubnum = 0;
                    foreach (string subsubcinfo in subcinfo.Cg_relateclass.Split(','))
                    {
                        if (subsubnum > 119) break;
                        if (subsubcinfo == "") continue;
                        string[] thesubstr = subsubcinfo.Split('|');
                        if (thesubstr.Length < 2) continue;
				 %>
				    <a title="<%=thesubstr[1]%>" href="goodslist-<%=thesubstr[0]%>.html"><%=thesubstr[1]%></a> 
				 <%
                     subsubnum += Utils.GetStringLength(thesubstr[1])+1;
                    }
                 %><a title="<%=subcinfo.Name%>" href="goodslist-p-<%=subcinfo.Cid%>.html">更多...</a>
				 </p>
			</li>
			<%
                }
                %>
		</ul>
	</div>
	<% 
        }
     %>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
<script type="text/javascript">
jQuery(document).ready(function(){
	jQuery(".caty").hover(function(){
		jQuery(this).removeClass().addClass("caty2 mar_top");
	},function(){
		jQuery(this).removeClass().addClass("caty mar_top");
	}); 
});
</script>
</asp:Content>

