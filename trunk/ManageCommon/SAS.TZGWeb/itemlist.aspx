<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="itemlist.aspx.cs" Inherits="itemlist" %>
<%@ Import Namespace="SAS.Entity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<!--内容开始-->
<div class="cot">
	<p class="site">您现在的位置：<a title="" href="index.html">淘之购</a> &gt; <a title="" href="list.shtml"><%=rootcategory.Name%></a> &gt; <%=parentcategory.Name%></p>
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
			<span class="listt9">
			关键字：<input type="text" name="textfield" class="input_jiaout" onblur="this.className='input_jiaout'" onfocus="this.className='input_jiaon'" style="width:150px;" />
			</span>
			<span class="listt9">
			价格：<input type="text" name="textfield" class="input_jiaout" onblur="this.className='input_jiaout'" onfocus="this.className='input_jiaon'" style="width:30px;" />
				-
				<input type="text" name="textfield" class="input_jiaout" onblur="this.className='input_jiaout'" onfocus="this.className='input_jiaon'" style="width:30px;" />
				<input type="submit" name="Submit" class="jia" value="确定" />
			</span>
			<p class="listtit2">
				<span>共<em class="f_f00">102</em>条</span>
				<span>1/52</span>
				<span class="listtit3"><a title="" href="#"><img alt="" src="images/arrow8.gif" /></a></span>
				<span class="listtit4"><a title="" href="#"><em>下一页</em><img alt="" src="images/arrow9.gif" /></a></span>
			</p>
		</div>
		<div class="listtit">
			<div class="listtnr">
				<p class="listtit1">
					<span class="listt2"><a title="切换到列表" href="javascript:void(0)">切换到列表</a></span>
					<span class="listt3"><a title="销量从高到低" href="javascript:void(0)">销量</a></span>
					<span class="listt3"><a title="信用从高到低" href="javascript:void(0)">信用</a></span>
					<span class="listt5"><a title="价格从低到高" href="javascript:void(0)">价格</a></span>
				</p>
				
			</div>
		</div>
		<ul class="listnr listbg">
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
		<ul class="listnr">
			<li>
				<a title="" href="show.shtml">
				<em class="listnrtu"><img alt="" src="images/ad/380x260.gif" /></em>
				<em class="listnrt">josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</em>
				</a>
				<p>已售出：<i class="zi">201</i>件</p>
				<strong>￥158.00</strong>
				<b>运费：8.00</b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank1 rankw5"></i></ins>
			</li>
			<li>
				<a title="" href="show.shtml">
				<em class="listnrtu"><img alt="" src="images/ad/380x260.gif" /></em>
				<em class="listnrt">josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</em>
				</a>
				<p>已售出：<i class="zi">201</i>件</p>
				<strong>￥158.00</strong>
				<b>运费：8.00</b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank3 rankw4"></i></ins>
			</li>
			<li>
				<a title="" href="show.shtml">
				<em class="listnrtu"><img alt="" src="images/ad/380x260.gif" /></em>
				<em class="listnrt">josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</em>
				</a>
				<p>已售出：<i class="zi">201</i>件</p>
				<strong>￥158.00</strong>
				<b>运费：8.00</b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank5 rankw1"></i></ins>
			</li>
			<li>
				<a title="" href="show.shtml">
				<em class="listnrtu"><img alt="" src="images/ad/380x260.gif" /></em>
				<em class="listnrt">josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</em>
				</a>
				<p>已售出：<i class="zi">201</i>件</p>
				<strong>￥158.00</strong>
				<b>运费：8.00</b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank6 rankw2"></i></ins>
			</li>
			<li>
				<a title="" href="show.shtml">
				<em class="listnrtu"><img alt="" src="images/ad/380x260.gif" /></em>
				<em class="listnrt">josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</em>
				</a>
				<p>已售出：<i class="zi">201</i>件</p>
				<strong>￥158.00</strong>
				<b>运费：8.00</b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank2 rankw4"></i></ins>
			</li>
			<li>
				<a title="" href="show.shtml">
				<em class="listnrtu"><img alt="" src="images/ad/380x260.gif" /></em>
				<em class="listnrt">josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</em>
				</a>
				<p>已售出：<i class="zi">201</i>件</p>
				<strong>￥158.00</strong>
				<b>运费：8.00</b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank4 rankw5"></i></ins>
			</li>
			<li>
				<a title="" href="show.shtml">
				<em class="listnrtu"><img alt="" src="images/ad/380x260.gif" /></em>
				<em class="listnrt">josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</em>
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
				</a>
				<p>已售出：<i class="zi">201</i>件</p>
				<strong>￥158.00</strong>
				<b>运费：8.00</b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank2 rankw5"></i></ins>
			</li>
			<li>
				<a title="" href="show.shtml">
				<em class="listnrtu"><img alt="" src="images/ad/380x260.gif" /></em>
				<em class="listnrt">josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</em>
				</a>
				<p>已售出：<i class="zi">201</i>件</p>
				<strong>￥158.00</strong>
				<b>运费：8.00</b>
				<ins>所属店铺信誉度：<br />
				<i class="zi2 rankbg rank3 rankw2"></i></ins>
			</li>
		</ul>
		<p class="page f_666">
			<a title="上一页" href="#">上一页</a>
			<a title="1" href="#">1</a>
			<span class="disabled">2</span>
			<a title="3" href="#">3</a>
			<a title="4" href="#">4</a>
			<a title="5" href="#">5</a>
			<a title="6" href="#">6</a>
			<a title="7" href="#">7</a>
			<a title="6" href="#">8</a>
			<a title="7" href="#">9</a>
			<a title="7" href="#">...</a>
			<a title="下一页" href="#">下一页</a>
		</p>
	</div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
<script type="text/javascript">
    jQuery(document).ready(function() {
        jQuery("#menu").menudrop();
        jQuery("#list").find(".listt2").click(function() {
            var listcss1 = jQuery("#list").find(".listt2").attr("class");
            if (listcss1 == 'listt2') {
                jQuery(this).removeClass().addClass("listt1");
                jQuery(this).find("a").html("切换到大图").attr("title", "切换到大图");
                jQuery("#list").find("ul").removeClass().addClass("listnr2");
                jQuery("#list").find("ul:first").removeClass().addClass("listnr2 listbg");
            } else {
                jQuery(this).removeClass().addClass("listt2");
                jQuery(this).find("a").html("切换到列表").attr("title", "切换到列表");
                jQuery("#list").find("ul").removeClass().addClass("listnr");
                jQuery("#list").find("ul:first").removeClass().addClass("listnr listbg");
            }
        });
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

