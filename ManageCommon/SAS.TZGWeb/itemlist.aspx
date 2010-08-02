<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="itemlist.aspx.cs" Inherits="itemlist" %>
<%@ Import Namespace="SAS.Entity" %>
<%@ Import Namespace="SAS.Entity.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<!--内容开始-->
<div class="cot">
	<p class="site">您现在的位置：<a title="" href="index.html">淘之购</a> &gt; <a title="" href="chanels_<%=rootcategory.Cid%>.html"><%=rootcategory.Name%></a> &gt; <%=parentcategory.Name%></p>
	<div class="listlt">
		<div class="listlt1" id="listmu">
			<h3><%=rootcategory.Name%></h3>
			<strong><em><%=parentcategory.Name%></em><img alt="打开" src="images/minus.gif" /></strong>
			<ul class="listlt1nr">
			<%
                foreach (string curstr in parentcategory.Cg_relateclass.Split(','))
                {
                    if (string.IsNullOrEmpty(curstr)) continue;
                    string[] curstrarray = curstr.Split('|');
                    if (curstrarray[0] == cid.ToString())
                    {
		    %>
		        <li class="listlt1bg"><a class="l_e00000" title="<%=curstrarray[1]%>" href="goodslist-<%=curstrarray[0]%>.html"><%=curstrarray[1]%></a></li>
		    <%
                    }
                    else
                    {
            %>
				<li><a title="<%=curstrarray[1]%>" href="goodslist-<%=curstrarray[0]%>.html"><%=curstrarray[1]%></a></li>
			<%
                    }
                }
            %>
			</ul>
			<%    
                foreach (CategoryInfo cinfo in itemlistcategories)
                {
                    if(cinfo.Cid == parentcategory.Cid)continue;
		    %>
			<strong><em><%=cinfo.Name%></em><img alt="打开" src="images/plus.gif" /></strong>
			<ul class="listlt1nr">
			<%
                foreach (string curstr in cinfo.Cg_relateclass.Split(','))
                {
                    if (string.IsNullOrEmpty(curstr)) continue;
                    string[] curstrarray = curstr.Split('|');
		    %>
				<li><a title="<%=curstrarray[1]%>" href="goodslist-<%=curstrarray[0]%>.html"><%=curstrarray[1]%></a></li>
			<%
                }
            %>
			</ul>
			<%
                }
			%>
		</div>
		<div class="listlt1 mar_top">
			<h3>品 牌</h3>
			<ul class="listlt1nr">
			    <%
                    foreach (GoodsBrandInfo ginfo in itemlistgoodsbrands)
                    {        
			    %>
				<li><a title="<%=ginfo.bname%>" href="list.shtml"><%=ginfo.bname%>/<%=ginfo.spell%></a></li>
				<%
                    }
                %>
			</ul>
		</div>
		<div class="listlt2 mar_top">
			<strong>浏览过的商品</strong>
			<ul class="listlt2nr">
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>￥120.00</span>
					</a>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>￥120.00</span>
					</a>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>￥120.00</span>
					</a>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>￥120.00</span>
					</a>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>￥120.00</span>
					</a>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>￥120.00</span>
					</a>
				</li>
			</ul>
		</div>
	</div>
	<div class="listrt" id="list">
		<div class="listrtit">
		    <form name="form1" onsubmit="return vaildform(this);">
			<span class="listt9">
			关键字：<input type="text" name="keyword" class="input_jiaout" onblur="this.className='input_jiaout'" onfocus="this.className='input_jiaon'" style="width:150px;" value="<%=keyword%>"/>
			</span>
			<span class="listt9">
			价格：<input type="text" name="startmoney" class="input_jiaout" onblur="this.className='input_jiaout'" onfocus="this.className='input_jiaon'" style="width:30px;" value="<%=startmoney%>"/>
				-
				<input type="text" name="endmoney" class="input_jiaout" onblur="this.className='input_jiaout'" onfocus="this.className='input_jiaon'" style="width:30px;" value="<%=endmoney%>"/>
				<input type="submit" class="jia" value="确定" />
			</span>
			</form>
			<p class="listtit2">
				<span>共<em class="f_f00"><%=itemcount%></em>条</span>
				<span><%=pageid%>/<%=pagecount%></span>
				<span class="listtit3"><a title="上一页" href="goodslist-<%=prevpage%>-<%=cid%>-<%=sortid%>-<%=viewtype%>-<%=startmoney%>-<%=endmoney%>-<%=searchkey%>.html"><img alt="" src="images/arrow8.gif" /></a></span>
				<span class="listtit4"><a title="下一页" href="goodslist-<%=nextpage%>-<%=cid%>-<%=sortid%>-<%=viewtype%>-<%=startmoney%>-<%=endmoney%>-<%=searchkey%>.html"><em>下一页</em><img alt="" src="images/arrow9.gif" /></a></span>
			</p>
		</div>
		<div class="listtit">
			<div class="listtnr">
				<p class="listtit1">
					<span class="listt<%=viewtype%2+1%>"><a title="<%=viewtype == 1 ? "切换到列表" : "切换到视图"%>" href="goodslist-<%=pageid%>-<%=cid%>-<%=sortid%>-<%=viewtype==1?2:1%>-<%=startmoney%>-<%=endmoney%>-<%=searchkey%>.html"><%=viewtype == 1 ? "切换到列表" : "切换到视图"%></a></span>
					<span class="listt<%=sortid==1?4:3%>"><a title="销量从高到低" href="goodslist-<%=pageid%>-<%=cid%>-<%=1%>-<%=viewtype%>-<%=startmoney%>-<%=endmoney%>-<%=searchkey%>.html">销量</a></span>
					<span class="listt<%=sortid==2?4:3%>"><a title="信用从高到低" href="goodslist-<%=pageid%>-<%=cid%>-<%=2%>-<%=viewtype%>-<%=startmoney%>-<%=endmoney%>-<%=searchkey%>.html">信用</a></span>
					<span class="listt<%=sortid==3?6:5%>"><a title="价格从低到高" href="goodslist-<%=pageid%>-<%=cid%>-<%=3%>-<%=viewtype%>-<%=startmoney%>-<%=endmoney%>-<%=searchkey%>.html">价格</a></span>
				</p>
				
			</div>
		</div>
		<ul class="<%=viewtype == 1 ? "listnr" : "listnr2"%> listbg">
			<li>
				<a title="" href="show.shtml">
				<em class="listnrtu"><img alt="" src="images/ad/380x260.gif" /></em>
				<em class="listnrt">josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</em>
				<span></span>
				</a>
				<p>已售出：<i class="zi">201</i>件</p>
				<strong>￥158.00</strong>
				<b>运费：8.00</b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank5 rankw2"></i></ins>
			</li>
			<li>
				<a title="" href="show.shtml">
				<em class="listnrtu"><img alt="" src="images/ad/380x260.gif" /></em>
				<em class="listnrt">josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</em>
				<span></span>
				</a>
				<p>已售出：<i class="zi">201</i>件</p>
				<strong>￥158.00</strong>
				<b>运费：8.00</b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank6 rankw5"></i></ins>
			</li>
			<li>
				<a title="" href="show.shtml">
				<em class="listnrtu"><img alt="" src="images/ad/380x260.gif" /></em>
				<em class="listnrt">josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</em>
				<span></span>
				</a>
				<p>已售出：<i class="zi">201</i>件</p>
				<strong>￥158.00</strong>
				<b>运费：8.00</b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank5 rankw4"></i></ins>
			</li>
		</ul>
		<ul class="<%=viewtype == 1 ? "listnr" : "listnr2"%>">
		<%
            foreach (TaobaokeItem tkinfo in itemlistitems)
            {
		%>
			<li>
				<a title="<%=tkinfo.Title%>" href="productshow-<%=tkinfo.NumIid%>.html">
				<em class="listnrtu"><img alt="" src="<%=tkinfo.PicUrl%>_b.jpg" /></em>
				<em class="listnrt"><%=tkinfo.Title%></em>
				</a>
				<p>已售出：<i class="zi"><%=tkinfo.CommissionNum%></i>件</p>
				<strong>￥<%=tkinfo.Price%></strong>
				<b>所在地：<%=tkinfo.ItemLocation%><br/>掌柜：<a title="<%=tkinfo.Nick%>" href="<%=tkinfo.ShopClickUrl%>"><%=tkinfo.Nick%></a></b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank<%=System.Math.Ceiling((double)tkinfo.SellerCreditScore / 5)%> rankw<%=tkinfo.SellerCreditScore % 5==0?5:tkinfo.SellerCreditScore % 5%>"></i></ins>
			</li>
			<%
            }
            %>
		</ul>
		<p class="page f_666">
			<%=pagenumbers%>
		</p>
	</div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
<script type="text/javascript">
    function vaildform(formobj) {
        if (formobj.startmoney.value!= "" && !isNumber(formobj.startmoney.value)) {
            alert("价格请输入整数！");
            return false;
        }
        if (formobj.endmoney.value != "" && !isNumber(formobj.endmoney.value)) {
            alert("价格请输入整数！");
            return false;
        }
        window.location.href = "goodslist-<%=pageid%>-<%=cid%>-<%=sortid%>-<%=viewtype%>-" + formobj.startmoney.value + "-" + formobj.endmoney.value + "-" + encodeURIComponent(formobj.keyword.value).replace("'", "%27") + ".html";
        return false;
    }

    jQuery(document).ready(function() {
        jQuery("#list").find(".listt3").click(function() {
            jQuery("#list").find(".listt4").removeClass().addClass("listt3");
            jQuery("#list").find(".listt6").removeClass().addClass("listt5");
            jQuery(this).removeClass().addClass("listt4");
        });
        jQuery("#list").find(".listt5").click(function() {
            jQuery("#list").find(".listt4").removeClass().addClass("listt3");
            jQuery(this).removeClass().addClass("listt6");
        });
        jQuery("#listmu").find("ul").hide();
        jQuery("#listmu").find("ul:first").show();
        jQuery("#listmu").find("strong").css("cursor", "pointer").click(function() {
            var listnr = jQuery(this).next();
            if (listnr.is(':visible')) {
                listnr.slideUp();
                jQuery(this).find("img").attr({ src: "images/plus.gif", alt: "打开" });
            } else {
                jQuery("#listmu").find("ul").slideUp();
                listnr.slideDown();
                jQuery("#listmu").find("img").attr({ src: "images/plus.gif", alt: "打开" });
                jQuery(this).find("img").attr({ src: "images/minus.gif", alt: "关闭" });
            }
        });

    });	
</script>
</asp:Content>

