<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="credit.aspx.cs" Inherits="credit" %>
<%@ Import Namespace="SAS.Entity"%>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<div class="cot">
	<ul class="credit">
	 <%
            int adinfo__id = 1;
            foreach (AdShowInfo adinfo in adlist1)
            {
                string[] astr = adinfo.Parameters.Split('|');
                if (astr.Length < 8) continue;
     %>
		<li class="cred1"><a target="_blank" title="<%=astr[5]%>" href="<%=astr[4]%>"><img alt="<%=astr[5]%>" src="<%=astr[1]%>" /></a></li>
		<%if(adinfo__id%3>0){%><li class="cred2"></li><%}%>		
	<%
            adinfo__id++;
         }
	%>
	</ul>
	<%
        int cinfo__id = 1;
        foreach (SAS.Entity.CategoryInfo cinfo in classlist)
        {   
	 %>
	<div class="crednr mar_top" id="crednr<%=cinfo__id%>">
		<strong><%=cinfo.Name%></strong>
		<ul class="credcot">
		    <%
                int sinfo__id = 1;
                foreach (SAS.Entity.ShopDetailInfo sinfo in GetShopList(cinfo.Cid))
                {
            %>
			<li class="<%=sinfo__id==1?"credcot2":"credcot1"%>">
				<a target="_blank" title="<%=sinfo.title%>" href="storeshow-<%=sinfo.sid%>.html">
				<em class="tu"><%=sinfo__id%></em>
				<b><img alt="<%=sinfo.title%>" src="<%=shoppic_path + sinfo.pic_path%>"/></b>
				<strong><%=sinfo.title%></strong>
				<span class="credcot1nr">
					<em class="credcot1x">收藏人气：1234</em>
					<em class="credcot1x">地址：<%=sinfo.shop_province+sinfo.shop_city%></em>
					<em class="credcot1x">好评率：<%=decimal.Round(decimal.Parse((((double)sinfo.good_num / (double)sinfo.total_num)*100).ToString()),2)%>%</em>
					<em class="credcot1x2 rankbg rank<%=System.Math.Ceiling((double)sinfo.shop_level / 5)%> rankw<%=sinfo.shop_level % 5==0?5:sinfo.shop_level % 5%>"></em>
				</span>
				</a>
			</li>
			<%
                sinfo__id++;
                }
                %>
		</ul>
	</div>
	<%if (cinfo__id % 3 != 0)
        {
       %>
	<div class="crednr2 mar_top"></div>
	<%
        }
       %>
	<%
        cinfo__id++;
        }
     %>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
<script type="text/javascript">
jQuery(document).ready(function() {
    var count = <%=classcount%>;
    for(var i = 1;i<count+1;i++){
        jQuery.slide.objid = "crednr" + i;
	    jQuery.slide.slides({cssout:"credcot1",csson:"credcot2"});
    }
});
</script>
</asp:Content>

