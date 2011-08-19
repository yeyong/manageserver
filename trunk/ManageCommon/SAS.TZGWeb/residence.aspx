<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="residence.aspx.cs" Inherits="residence" %>
<%@ Import Namespace="SAS.Entity"%>
<%@ Import Namespace="SAS.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
<script src="js/jquery-scroll.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<div class="cot">
	<div class="res">
		<div class="reslt">
			<p class="tp"><%string[] indexad1 = adlist1.Split('|');if(indexad1.Length >=8){%><a target="_blank" title="<%=indexad1[5]%>" href="<%=indexad1[4]%>"><img alt="<%=indexad1[5]%>" src="<%=indexad1[1]%>" /></a><%}%></p>
			<p class="bt">
				<span><img src="images/res_09.jpg" alt="" border="0" usemap="#Map" /></span>
				<span><map name="Map" id="Map">
					<area shape="rect" coords="53,10,194,64" href="#res1" />
					<area shape="rect" coords="52,69,194,118" href="#res2" />
					<area shape="rect" coords="52,122,195,171" href="#res3" />
				</map></span>
			</p>
		</div>
		<div class="resrt" id="restu">
			<p class="tp"></p>
			<div class="ce">
				<p class="ce1"></p>
				<div class="ce2" id="dtu">
					<%if(adlist2.Length>0){if(adlist2[0].Parameters.Split('|').Length>=8){%><a target="_blank" title="<%=adlist2[0].Parameters.Split('|')[5]%>" href="<%=adlist2[0].Parameters.Split('|')[4]%>"><img alt="<%=adlist2[0].Parameters.Split('|')[5]%>" src="<%=adlist2[0].Parameters.Split('|')[1]%>" /></a><%}}%>
				</div>
				<p class="ce3"></p>
			</div>
			<div class="bt">
				<p class="arrow-lt" id="btn1"><img alt="向左" src="images/res-arrow-lt.gif" /></p>
				<div id="branr">
					<ul class="nr">
					<%
                        int adinfo__id = 1;    
                        foreach (AdShowInfo adinfo in adlist2)
                        {
                            if (adinfo__id > 8) break;
                            string[] astr = adinfo.Parameters.Split('|');
                            if (astr.Length < 8) continue;
		            %>
			            <li class="<%=(adinfo__id==1?"nr2":"nr1")%>"><a title="" href="javascript:void(0)"><img rel="<%=astr[4]%>" alt="<%=astr[5]%>" src="<%=astr[1]%>" /></a></li>
			            <%
                            adinfo__id++;
                        }
                        %>
					</ul>
				</div>
				<p class="arrow-lt" id="btn2"><img alt="向右" src="images/res-arrow-rt.gif" /></p>
			</div>
		</div>
	</div>
	<div class="res" id="res1"><img alt="美容顾问来家访" src="images/res_11.jpg" /></div>
	<div class="res">
		<p class="res2lt"></p>
		<div class="res2ce">
			<iframe frameborder="0" marginheight="0" marginwidth="0" border="0" id="alimamaifrm" name="alimamaifrm" scrolling="no" height="150px" width="760px" src="http://taoke.alimama.com/channel/beautifyChannelHor.htm?pid=mm_13451138_0_0" ></iframe>
		</div>
		<p class="res2rt"></p>
	</div>
	<div class="res" id="res2">
		<p class="res3lt"></p>
		<ul class="res3ce">
		<%
		    int adinfo3__id = 1;
            foreach (AdShowInfo adinfo6 in adlist3)
            {
                if (adinfo3__id > 5) break;
                string[] astr = adinfo6.Parameters.Split('|');
                if (astr.Length < 8) continue;
		%>
			<li class="<%=(adinfo3__id%2==1?"tp1":"tp2")%>"><a target="_blank" title="<%=astr[5]%>" href="<%=astr[4]%>"><img alt="<%=astr[5]%>" src="<%=astr[1]%>" /></a></li>
		<%
            adinfo3__id++;
            }
		%>
		</ul>
		<p class="res3rt"></p>
	</div>
	<div class="res" id="res3"><img alt="足不出户 尽享收获 试衣间" src="images/res_18.jpg" /></div>
	<div class="res">
		<div class="res4">
			<div class="res4nr">
			<iframe frameborder="0" marginheight="0" marginwidth="0" border="0" id="alimamaifrm" name="alimamaifrm" scrolling="no" height="670" width="780" src="http://www.taobao.com/go/act/shiyi/alishiyi.php?t=tk&pid=mm_13451138_2368524_9228688" ></iframe>
			</div>
		</div>
	</div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
<script type="text/javascript"> 
jQuery(document).ready(function(){
	jQuery("#branr").Scroll({line:4,speed:300,left:"btn2",right:"btn1"});
	jQuery("#restu").find("li").click(function() {
	    jQuery("#restu").find("li").removeClass().addClass("nr1");
	    jQuery(this).removeClass().addClass("nr2");
	    var swosrc = jQuery(this).find("img").attr("src");
	    var swoalt = jQuery(this).find("img").attr("alt");
	    var swohref = jQuery(this).find("img").attr("rel");
	    jQuery("#dtu").find("img").attr("src", swosrc);
	    jQuery("#dtu").find("img").attr("alt", swoalt);
	    jQuery("#dtu").find("a").attr("href", swohref);
	});
});	
</script>
</asp:Content>

