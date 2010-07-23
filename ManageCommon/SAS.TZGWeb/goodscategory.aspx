<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="goodscategory.aspx.cs" Inherits="goodscategory" %>
<%@ Import Namespace="SAS.Entity" %>
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
                    foreach (string subsubcinfo in subcinfo.Cg_relateclass.Split(','))
                    {
                        if (subsubcinfo != "")
                        {
				 %>
				    <a title="" href="goodslist-<%=subsubcinfo.Split('|')[0]%>.html"><%=subsubcinfo.Split('|')[1]%></a>  
				 <%
                        } 
                    }
                 %>
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

