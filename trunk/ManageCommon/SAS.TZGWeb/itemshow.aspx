<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" CodeFile="itemshow.aspx.cs" Inherits="itemshow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<link href="css/channels.css" rel="stylesheet" type="text/css" />
<link href="css/jquery.cluetip.css" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
<script src="js/jquery.cluetip.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<div class="cot">
	<p class="site">您现在的位置：<a title="" href="index.shtml">淘之购</a> &gt; <a title="" href="list.shtml">户外运动</a> &gt; <a title="" href="list.shtml">运动鞋</a> &gt; 今夏完美时尚热卖韩版 高腰显瘦衬衣 短袖翻领格子女 衬衫 送腰带</p>
	<div class="listlt">
		<div class="listlt1">
			<h3>相关类别</h3>
			<ul class="listlt1nr">
				<li><a title="" href="list.shtml">运动休闲鞋(1774)</a></li>
				<li><a title="" href="list.shtml">跑步鞋(1153)</a></li>
				<li><a title="" href="list.shtml">复古鞋/板鞋(1121)</a></li>
				<li><a title="" href="list.shtml">篮球鞋(475)</a></li>
				<li><a title="" href="list.shtml">训练鞋(358)</a></li>
				<li><a title="" href="list.shtml">网球鞋(302)</a></li>
				<li><a title="" href="list.shtml">运动休闲鞋(1774)</a></li>
				<li><a title="" href="list.shtml">跑步鞋(1153)</a></li>
				<li><a title="" href="list.shtml">复古鞋/板鞋(1121)</a></li>
				<li><a title="" href="list.shtml">篮球鞋(475)</a></li>
				<li><a title="" href="list.shtml">训练鞋(358)</a></li>
				<li><a title="" href="list.shtml">网球鞋(302)</a></li>
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
	<div class="listrt">
		<p class="showtu"><img alt="<%=iteminfo.Title%>" src="<%=iteminfo.PicUrl%>_310x310.jpg" /></p>
		<div class="showrt">
			<h1><%=iteminfo.Title%></h1>
			<p>价格：<em class="zi">￥<%=iteminfo.Price%></em></p>
			<p>已售出：<em class="f_f00"><%=iteminfo.Volume%></em><%=iteminfo.Cid%> 件</p>
			<p>
				<span>商品数：<%=iteminfo.Num%></span>
			</p>
			<em class="showline"></em>
			<p>掌柜店铺：<a title="<%=iteminfo.Nick%>" class="l_f18c08" href="<%=tkitem.ShopClickUrl%>"><%=iteminfo.Nick%></a></p>
			<p>
				<span><i>累计信用：</i><b class="rankbg rank<%=System.Math.Ceiling((double)tkitem.SellerCreditScore / 5)%> rankw<%=tkitem.SellerCreditScore % 5==0?5:tkitem.SellerCreditScore % 5%>"></b></span>
				<span>好评率：<em class="f_f00">100%</em></span>
			</p>
			<p>所在地区：<%=tklocation.State+tklocation.City%></p>
			<p class="showan">
				<a title="<%=iteminfo.Title%>" href="<%=tkitem.ClickUrl%>"><img alt="点击查看详情" src="images/show_an1.gif" /></a>
				<a id="put" title="复制链接给好友" href="tooltip.html" rel="tooltip.html"><img alt="点击复制代码 分享给好友" src="images/show_an2.gif" /></a>
			</p>
		</div>
		<div class="showcon mar_top">
			<strong class="showcont">商品介绍</strong>
			<%=iteminfo.Desc%>
		</div>
		<div class="shownr mar_top">
			<strong>商品所在店铺的其他商品</strong>
			<ul class="showcot">
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</span>
					</a>
					<b>￥158.00</b>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</span>
					</a>
					<b>￥158.00</b>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</span>
					</a>
					<b>￥158.00</b>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</span>
					</a>
					<b>￥158.00</b>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</span>
					</a>
					<b>￥158.00</b>
				</li>
			</ul>
		</div>
		<div class="shownr mar_top">
			<strong>同类商品</strong>
			<ul class="showcot">
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</span>
					</a>
					<b>￥158.00</b>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</span>
					</a>
					<b>￥158.00</b>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</span>
					</a>
					<b>￥158.00</b>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</span>
					</a>
					<b>￥158.00</b>
				</li>
				<li>
					<a title="" href="show.shtml">
					<img alt="" src="images/ad/120x120.gif" />
					<span>josiny 杂志款漆皮凉靴 复古 罗马战靴 高跟凉鞋女鞋</span>
					</a>
					<b>￥158.00</b>
				</li>
			</ul>
		</div>
	</div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
<script type="text/javascript"> 
var nums = 5;
function closeCurrent(){
	if(nums < 0) nums = 5;
	jQuery('#nums').html(nums);
	jQuery('#copys').hide();
	jQuery('#result').show();
	if(nums > 0){			
		setTimeout("nums = nums - 1;closeCurrent();",1000);
	}else{
		nums = nums - 1;
		cluetip.style.display = 'none';
	}
}
function cancelCurrent(){
	cluetip.style.display = 'none';
}
jQuery(document).ready(function(){
	jQuery('#put').cluetip({ activation: 'click', sticky: true, width: 350, positionBy: 'bottomTop', closePosition: 'title',closeText: '<img src="images/cross.png" alt="close" />',cursor: 'pointer', dropShadow: false});
});	
</script>
</asp:Content>

