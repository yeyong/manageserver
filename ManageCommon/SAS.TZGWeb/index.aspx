<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="index.aspx.cs" Inherits="index" %>
<%@ Import Namespace="SAS.Entity"%>
<%@ Import Namespace="SAS.Common" %>
<asp:Content ID="Content0" ContentPlaceHolderID="styles" runat="server">
<link href="css/index.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" Runat="Server">
<script src="js/scrollPic.js" type="text/javascript"></script>
<script src="js/jquery-exchange.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainbody" Runat="Server">
<div class="cot">
	<div class="ban mar_top_10" id="banner">
		<p class="bantu"><%if(adlist1.Length>0){if(adlist1[0].Parameters.Split('|').Length>=8){%><a target="_blank" title="<%=adlist1[0].Parameters.Split('|')[5]%>" href="<%=adlist1[0].Parameters.Split('|')[4]%>"><img alt="<%=adlist1[0].Parameters.Split('|')[5]%>" src="<%=adlist1[0].Parameters.Split('|')[1]%>" /></a><%}}%></p>
		<ul class="banxtu">
		<%
            int adinfo__id = 1;    
            foreach (AdShowInfo adinfo in adlist1)
            {
                if (adinfo__id > 3) break;
                string[] astr = adinfo.Parameters.Split('|');
                if (astr.Length < 8) continue;
		%>
			<li><img rel="<%=astr[4]%>" alt="<%=astr[5]%>" src="<%=astr[1]%>" /></li>
			<%
                adinfo__id++;
            }
            %>
		</ul>
	</div>
	<div class="hpin mar_top_10">
		<strong>最新活动</strong>
		<ul class="hpinnr">
		<%
            int ainfo__id = 1;    
            foreach (ActivityInfo ainfo in taoactlist)
            {
                if (ainfo__id > 6) break;
		%>
			<li>[<em><%=SAS.Common.Utils.GetStandardDate(ainfo.Begintime)%></em>] <a target="_blank" title="<%=ainfo.Atitle%>" href="actshow-<%=ainfo.Id%>.html"><%=ainfo.Atitle%></a></li>
		<%
                ainfo__id++;
            }
		%>
		</ul>
		<p class="onead mar_top_5"><%string[] indexad2 = adlist2.Split('|');if(indexad2.Length >=8){%><a target="_blank" title="<%=indexad2[5]%>" href="<%=indexad2[4]%>"><img alt="<%=indexad2[5]%>" src="<%=indexad2[1]%>" /></a><%}%></p>
	</div>
</div>
<div class="cot">
	<div class="pref mar_top">
		<strong><img alt="优惠活动场" src="images/index_tit1.gif" /></strong>
		<em><a title="" href="topics.html">查看更多专题&gt;&gt;</a></em>
		<div class="prefnr">
			<p><%string[] indexad3 = adlist3.Split('|');if(indexad3.Length >=8){%><a target="_blank" title="<%=indexad3[5]%>" href="<%=indexad3[4]%>"><img alt="<%=indexad3[5]%>" src="<%=indexad3[1]%>" /></a><%}%></p>
			<p><%string[] indexad4 = adlist4.Split('|');if(indexad4.Length >=8){%><a target="_blank" title="<%=indexad4[5]%>" href="<%=indexad4[4]%>"><img alt="<%=indexad4[5]%>" src="<%=indexad4[1]%>" /></a><%}%></p>
			<p><%string[] indexad5 = adlist5.Split('|');if(indexad5.Length >=8){%><a target="_blank" title="<%=indexad5[5]%>" href="<%=indexad5[4]%>"><img alt="<%=indexad5[5]%>" src="<%=indexad5[1]%>" /></a><%}%></p>
			<ul class="prefzi">
			<%
                int indextopicinfo__id = 1;	    
                foreach (TaoBaoTopicInfo indextopicinfo in indextopiclist)
                {
                    if (indextopicinfo__id > 9) break;
		    %>
				<li><a target="_blank" title="<%=indextopicinfo.Title%>" href="topicshow-<%=indextopicinfo.Tid%>.html"><%=indextopicinfo.Title%></a></li>
				<%if (indextopicinfo__id % 3 == 0){%><li></li><%}%>
				<%
                    indextopicinfo__id++;
                }
                %>
			</ul>
		</div>
	</div>
	<div class="twoad" id="twoad">
		<div id="twoadnr">
		<%
		    int adinfo6__id = 1;
            foreach (AdShowInfo adinfo6 in adlist6)
            {
                if (adinfo6__id > 3) break;
                string[] astr = adinfo6.Parameters.Split('|');
                if (astr.Length < 8) continue;
		%>
			<div class="con"><a target="_blank" title="<%=astr[5]%>" href="<%=astr[4]%>"><img alt="<%=astr[5]%>" src="<%=astr[1]%>" /></a></div>
		<%
            adinfo6__id++;
            }
		%>
		</div>
		<ul id="twoadtit">
		    <%for(int sumi = adinfo6__id-1;sumi>0;sumi--){%><li></li><%}%>
		</ul>
	</div>
</div>
<div class="cot" id="like">
	<div class="liketit mar_top">
		<strong><img alt="潮我喜欢" src="images/index_tit2.gif" /></strong>
		<ul id="likert">
			<li>衣</li>
			<li>鞋</li>
			<li>饰</li>
			<li>妆</li>
			<li>居</li>
		</ul>
	</div>
	<div id="likenr">
		<div class="con">
		    <%
		    int adinfo7__id = 1;
            foreach (AdShowInfo adinfo7 in adlist7)
            {
                if (adinfo7__id > 5) break;
                string[] astr = adinfo7.Parameters.Split('|');
                if (astr.Length < 8) continue;      
		    %>
			<p class="mar<%=adinfo7__id%>"><a target="_blank" title="<%=astr[5]%>" href="<%=astr[4]%>"><img alt="<%=astr[5]%>" src="<%=astr[1]%>" /></a></p>
			<%
                adinfo7__id++;
            }
			%>
			<span></span>
		</div>
		<div class="con">
			<%
		    int adinfo8__id = 1;
            foreach (AdShowInfo adinfo8 in adlist8)
            {
                if (adinfo8__id > 5) break;
                string[] astr = adinfo8.Parameters.Split('|');
                if (astr.Length < 8) continue;      
		    %>
			<p class="mar<%=adinfo8__id%>"><a target="_blank" title="<%=astr[5]%>" href="<%=astr[4]%>"><img alt="<%=astr[5]%>" src="<%=astr[1]%>" /></a></p>
			<%
                adinfo8__id++;
            }
			%>
			<span></span>
		</div>
		<div class="con">
			<%
		    int adinfo9__id = 1;
            foreach (AdShowInfo adinfo9 in adlist9)
            {
                if (adinfo9__id > 5) break;
                string[] astr = adinfo9.Parameters.Split('|');
                if (astr.Length < 8) continue;      
		    %>
			<p class="mar<%=adinfo9__id%>"><a target="_blank" title="<%=astr[5]%>" href="<%=astr[4]%>"><img alt="<%=astr[5]%>" src="<%=astr[1]%>" /></a></p>
			<%
                adinfo9__id++;
            }
			%>
			<span></span>
		</div>
		<div class="con">
			<%
		    int adinfo10__id = 1;
            foreach (AdShowInfo adinfo10 in adlist10)
            {
                if (adinfo10__id > 5) break;
                string[] astr = adinfo10.Parameters.Split('|');
                if (astr.Length < 8) continue;      
		    %>
			<p class="mar<%=adinfo10__id%>"><a target="_blank" title="<%=astr[5]%>" href="<%=astr[4]%>"><img alt="<%=astr[5]%>" src="<%=astr[1]%>" /></a></p>
			<%
                adinfo10__id++;
            }
			%>
			<span></span>
		</div>
		<div class="con">
			<%
		    int adinfo11__id = 1;
            foreach (AdShowInfo adinfo11 in adlist11)
            {
                if (adinfo11__id > 5) break;
                string[] astr = adinfo11.Parameters.Split('|');
                if (astr.Length < 8) continue;      
		    %>
			<p class="mar<%=adinfo11__id%>"><a target="_blank" title="<%=astr[5]%>" href="<%=astr[4]%>"><img alt="<%=astr[5]%>" src="<%=astr[1]%>" /></a></p>
			<%
                adinfo11__id++;
            }
			%>
			<span></span>
		</div>
	</div>
</div>
<div class="cot">
	<ul class="inxbrd mar_top">
	<%
        foreach (GoodsBrandInfo ginfo in ginfolist)
        {
	%>
		<li><a title="<%=ginfo.bname%>" href="goodssearch-s-<%=Utils.UrlEncode(ginfo.bname)%>.html"><img alt="<%=ginfo.bname%>" src="<%=ginfo.logo%>" /></a></li>
	<%
        }
    %>
	</ul>
</div>
<div class="cot">
	<div class="three">
		<p class="threead mar_top"><%string[] indexad12 = adlist12.Split('|');if(indexad12.Length >=8){%><a target="_blank" title="<%=indexad12[5]%>" href="<%=indexad12[4]%>"><img alt="<%=indexad12[5]%>" src="<%=indexad12[1]%>" /></a><%}%></p>
		<div class="crown mar_top mar_left">
			<p class="crowntit">
				<strong><img alt="皇冠店铺" src="images/index_tit3.gif" /></strong>
				<em><a title="" href="credit.html">查看更多店铺&gt;&gt;</a></em>
			</p>
			<div class="crownr">
				<p class="crownlt" id="LeftArr"></p>
				<ul class="crowncot" id="crown">
				<%
                    int sinfo__id = 1;		    
                    foreach (ShopDetailInfo sinfo in shoplist)
                    {		    
			    %>
					<li>
						<a target="_blank" title="<%=sinfo.title%>" href="storeshow-<%=sinfo.sid%>.html">
						<p><img alt="<%=sinfo.title%>" src="<%=shoppic_path + sinfo.pic_path%>" /></p>
						<span><%=sinfo.title%></span>
						</a>
						<strong><ins class="rankbg rank<%=System.Math.Ceiling((double)sinfo.shop_level / 5)%> rankw<%=sinfo.shop_level % 5==0?5:sinfo.shop_level % 5%>"></ins></strong>
						<em>好评率：<i class="f_f00"><%=decimal.Round(decimal.Parse((((double)sinfo.good_num / (double)sinfo.total_num)*100).ToString()),2)%>%</i></em>
						<em>所在地：<%=sinfo.shop_province+sinfo.shop_city%></em>
						<em>收藏数：<i class="f_f00">1258</i></em>
						<b><%=sinfo__id%></b>
					</li>
					<%if (sinfo__id % 4 > 0){%><li class="li2"></li><%}%>
				<%
                        sinfo__id++;
                    }
				%>
				</ul>
				<p class="crownrt" id="RightArr"></p>
			</div>
		</div>
	</div>
	<ul class="brand mar_top" id="brand">
	    <%
		    int adinfo13__id = 1;
            foreach (AdShowInfo adinfo13 in adlist13)
            {
                if (adinfo13__id > 8) break;
                string[] astr = adinfo13.Parameters.Split('|');
                if (astr.Length < 8) continue;      
		%>
		<li style="background:url(<%=astr[1]%>) no-repeat; "><a target="_blank" title="<%=astr[5]%>" href="<%=astr[4]%>"></a></li>
		<%
                adinfo13__id++;
            }
		%>
	</ul>
</div>
<div class="cot">
	<p class="link mar_top">
		<strong>友 情链 接</strong>
		<span><%foreach(FriendLinkInfo finfo in flinklist){%><a class="l_666" target="_blank" title="<%=finfo.name%>" href="<%=finfo.linkurl%>"><%=finfo.name%></a> | <%}%></span>
		<em></em>
	</p>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
<script type="text/javascript">
    MYScrollPic("crown", 460, 460, 10, 100, true, 6); //调用方法
    jQuery(document).ready(function() {
        jQuery("#twoad").Exchange({ MIDS: "twoadtit", CIDS: "twoadnr", count: 3, mousetype: 1 });
        jQuery("#like").Exchange({ MIDS: "likert", CIDS: "likenr", count: 5, timer: 8000, mousetype: 1 });
        var banobj = jQuery('#banner').find("p");
        jQuery('#banner').find("li").mouseover(function() {
            banobj.find("a").attr("href", jQuery(this).find("img").attr("rel"));
            banobj.find("a").attr("title", jQuery(this).find("img").attr("alt"));
            banobj.find("img").attr("alt", jQuery(this).find("img").attr("alt"));
            banobj.find("img").attr("src", jQuery(this).find("img").attr("src"));
        });
        jQuery('#brand').find("li:first").css("background-position", "bottom");
        jQuery('#brand').find("li").mousemove(function() {
            jQuery('#brand').find("li").css("background-position", "top");
            jQuery(this).css("background-position", "bottom");
        });
    });
</script>
</asp:Content>

