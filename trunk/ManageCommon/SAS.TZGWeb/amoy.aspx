<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="amoy.aspx.cs" Inherits="amoy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="styles" Runat="Server">
<style type="text/css">
@charset "utf-8";
body{ font:12px 宋体,Verdana, Arial; background:#fff; margin:0px; padding:0px;}
span,p,ol,ul,li,dt,dl,dd,em,a,h1,h2,h3,h4,h5,h6,i,iframe,center,del,ins{ margin:0px; padding:0px;}
ul,li{list-style-type:none;}
em{ font-style:normal;}
img{ border:0px;}
img a:link,a:visited,a:hover,a:active{ blr:expression(this.onFocus=this.blur());}
img a:focus{ -moz-outline-style: none;}
.line{ line-height:1px; overflow:hidden; display:inline;}
.cot{ width:960px; height:auto; margin:0 auto;}
.nr{ width:960px; height:auto; float:left;}
.nr div{ float:left;}
.nr p{ float:left;}
.nr span{ float:left; position:relative;}
.con{ width:960px; height:auto; padding:5px 0; float:left;}
.bg1{ background:#e6f2f2;}
.bg1 strong{ width:960px; height:29px; text-align:center; background:url(http://files.zheshangonline.com/20100806/main_09.gif) repeat-x; padding:15px 0 16px 0; float:left;}
.bg2{ background:#e9edec;}
.bg2 strong{ width:960px; height:29px; text-align:center; background:url(http://files.zheshangonline.com/20100806/main_10.gif) repeat-x; padding:15px 0 16px 0; float:left;}
.nr1{ width:960px; height:260px; float:left;}
.nr1 li{ width:160px; height:230px; margin:15px 5px 0 23px; display:inline; float:left;}
.nr1 li a{ width:160px; height:205px; float:left; color:#000; text-decoration:none; cursor:pointer;}
.nr1 li a:hover{ width:160px; height:205px; float:left; color:#e00000; text-decoration:underline; cursor:pointer;}
.nr1 li img{ width:160px; height:160px; float:left;}
.nr1 li em{ width:150px; height:40px; line-height:20px; margin-top:5px; display:inline; padding:0 5px 0 5px; float:left;}
.nr1 li b{ width:150px; height:25px; line-height:25px;color:#fe0142; font-size:18px; font-family:Georgia, "Times New Roman", Times, serif; padding:0 5px 0 5px; float:left;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainbody" Runat="Server">
<div class="cot">
	<p class="nr mar_top">
		<span><img src="http://files.zheshangonline.com/20100806/main_01.jpg" alt="泡沫之夏 夏日必备单品" /></span>
		<span><img src="http://files.zheshangonline.com/20100806/main_02.jpg" alt="泡沫之夏 夏日必备单品" border="0" usemap="#Map" /></span>
		<span><map name="Map" id="Map">
			<area shape="poly" coords="24,148,71,127,103,116,103,130,211,130,211,61,98,61,98,85,24,85" title="平沿帽子 夏天 遮阳帽 草编" target="_blank" href="http://taogou.zheshangonline.com/productshow-5826812013.html" />
			<area shape="poly" coords="225,116,247,127,258,108,292,125,317,142,334,148,331,134,316,106,302,86,300,78,282,72,262,102,247,101,240,99,231,106" title="平沿帽子 夏天 遮阳帽 草编" target="_blank" href="http://taogou.zheshangonline.com/productshow-5826812013.html" />
		</map></span>
	</p>
	<p class="nr">
		<span><img src="http://files.zheshangonline.com/20100806/main_03.jpg" alt="浪漫七夕" border="0" usemap="#Map2" /></span>
		<span><map name="Map2" id="Map2">
			<area shape="rect" coords="27,65,344,132" title="夏日妆容必备" href="#prd1" />
			<area shape="poly" coords="26,148,316,147,316,216,171,216,171,290,26,290" title="深层美白祛斑精华液正品 去斑去痘印anr补水左旋C包邮" target="_blank" href="http://taogou.zheshangonline.com/productshow-4911517483.html" />
			<area shape="poly" coords="362,98,526,98,526,163,446,163,446,282,320,283,320,151,362,151" title="PBA柔肤全效BB霜 正品 防晒隔离 保养遮瑕 保 湿修复 顶级裸妆王" target="_blank" href="http://taogou.zheshangonline.com/productshow-3691047764.html" />
			<area shape="poly" coords="722,95,752,102,785,105,827,98,856,89,851,-1,938,-2,938,48,880,46,867,117,885,169,891,204,906,272,908,290,691,291" title="波西米亚长裙" target="_blank" href="http://taogou.zheshangonline.com/productshow-3128301114.html" />
		</map></span>
	</p>
	<p class="nr">
		<span><img src="http://files.zheshangonline.com/20100806/main_04.gif" alt="浪漫七夕" border="0" usemap="#Map3" /></span>
		<span><map name="Map3" id="Map3">
			<area shape="poly" coords="5,60,86,60,86,8,265,8,265,55,183,55,183,96,138,96,138,227,5,227" title="百分百正品柠梅系列之 绿色减肥★安全-绿色 -高效" target="_blank" href="http://taogou.zheshangonline.com/productshow-2822264333.html" />
			<area shape="poly" coords="209,112,349,112,349,253,421,253,421,320,209,320" title="热卖太阳城 防紫外线 双层遮阳伞太阳伞" target="_blank" href="http://taogou.zheshangonline.com/productshow-1573445978.html" />
			<area shape="rect" coords="319,46,629,98" title="夏日生活必备" href="#prd2" />
			<area shape="rect" coords="81,344,390,389" title="夏日美装必备" href="#prd3" />
			<area shape="poly" coords="430,170,496,170,496,121,707,121,707,191,635,191,635,340,430,340" title="嫩肤杀菌TJ1088释压慢回弹 颈椎护理记忆枕头" target="_blank" href="http://taogou.zheshangonline.com/productshow-2589434443.html" />
			<area shape="poly" coords="650,306,686,306,686,247,749,178,930,178,930,374,650,374" title="日本钛锗银防晒披肩3色 防晒披风防紫外线丝巾手臂套" target="_blank" href="http://taogou.zheshangonline.com/productshow-2630444789.html" />
		</map></span>
	</p>
	<p class="nr">
		<span><img src="http://files.zheshangonline.com/20100806/main_05.gif" alt="浪漫七夕" border="0" usemap="#Map4" /></span>
		<span><map name="Map4" id="Map4">
			<area shape="poly" coords="13,18,366,18,366,64,207,64,207,89,162,89,162,178,13,178" title="夏天回忆 10年新 韩国明星 JEWERLY可爱比基尼泳装钢圈泳衣" target="_blank" href="http://taogou.zheshangonline.com/productshow-3061954657.html" />
			<area shape="poly" coords="14,254,159,254,159,170,274,170,274,252,312,252,312,384,14,384" title="韩国正品防晒衣 长袖超薄开 衫透明防晒小外套外搭" target="_blank" href="http://taogou.zheshangonline.com/productshow-2062478056.html" />
			<area shape="poly" coords="292,75,383,75,383,7,580,7,580,53,481,53,481,240,292,240" title="韩版休闲 新款泳装/10088 时尚分体泳衣3件套" target="_blank" href="http://taogou.zheshangonline.com/productshow-3854675480.html" />
			<area shape="poly" coords="537,60,760,60,760,114,664,114,664,253,623,253,589,287,494,287,494,202,537,202" title="疯卖 NY帽子 休闲/太阳帽 男/棒 球帽/遮阳帽 帽子 女 夏天" target="_blank" href="http://taogou.zheshangonline.com/productshow-2695661404.html" />
			<area shape="rect" coords="337,326,647,372" title="夏日配饰必备" href="#prd4" />
			<area shape="poly" coords="753,155,782,116,782,3,924,3,924,187,832,187,832,211,778,211,778,177" title="男士夹趾沙滩皮凉鞋 运动休闲凉拖两用" target="_blank" href="http://taogou.zheshangonline.com/productshow-46252709.html" />
			<area shape="poly" coords="616,261,769,261,769,302,839,261,947,261,947,385,748,385,748,317,605,317" title="波西米亚罗马串珠人字拖夹 脚亚麻平底夹指淑女凉拖" target="_blank" href="http://taogou.zheshangonline.com/productshow-5858626794.html" />
		</map></span>
	</p>
	<div class="con bg1" id="prd1">
		<strong><img title="夏日妆容必备" src="http://files.zheshangonline.com/20100806/main_08.gif" /></strong>
		<ul class="nr1">
			<li>
				<a target="_blank" title="包快递何氏狐臭净20ML出口 标准装2010新货 包正品" href="http://taogou.zheshangonline.com/productshow-3111065149.html">
					<img alt="包快递何氏狐臭净20ML出口 标准装2010新货 包正品" src="http://files.zheshangonline.com/20100806/main_12.gif" />
					<em>包快递何氏狐臭净20ML出口 标准装2010新货 包正品</em>
				</a>
				<b>￥173</b>
			</li>
			<li>
				<a target="_blank" title="买一送一再包邮 Coppertone 水宝宝防晒喷雾SPF50防晒霜" href="http://taogou.zheshangonline.com/productshow-4903032511.html">
					<img alt="买一送一再包邮 Coppertone 水宝宝防晒喷雾SPF50防晒霜" src="http://files.zheshangonline.com/20100806/main_14.gif" />
					<em>买一送一再包邮 Coppertone 水宝宝防晒喷雾SPF50防晒霜</em>
				</a>
				<b>￥53</b>
			</li>
			<li>
				<a target="_blank" title="芳草集 甘草排毒保湿面膜 水洗 睡眠面膜 补水 美白" href="http://taogou.zheshangonline.com/productshow-2253086530.html">
					<img alt="芳草集 甘草排毒保湿面膜 水洗 睡眠面膜 补水 美白" src="http://files.zheshangonline.com/20100806/main_16.gif" />
					<em>芳草集 甘草排毒保湿面膜 水洗 睡眠面膜 补水 美白</em>
				</a>
				<b>￥55</b>
			</li>
			<li>
				<a target="_blank" title="[御泥坊]清爽矿物泥浆面膜 控油清洁 去黑头 收缩毛孔" href="http://taogou.zheshangonline.com/productshow-2224198225.html">
					<img alt="[御泥坊]清爽矿物泥浆面膜 控油清洁 去黑头 收缩毛孔" src="http://files.zheshangonline.com/20100806/main_18.gif" />
					<em>[御泥坊]清爽矿物泥浆面膜 控油清洁 去黑头 收缩毛孔</em>
				</a>
				<b>￥99</b>
			</li>
			<li>
				<a target="_blank" title="西木博士推荐 左旋肉碱 健康 瘦身60粒装 买2送1" href="http://taogou.zheshangonline.com/productshow-6169893119.html">
					<img alt="西木博士推荐 左旋肉碱 健康 瘦身60粒装 买2送1" src="http://files.zheshangonline.com/20100806/main_20.gif" />
					<em>西木博士推荐 左旋肉碱 健康 瘦身60粒装 买2送1</em>
				</a>
				<b>￥168</b>
			</li>
		</ul>
	</div>
	<div class="con bg2" id="prd2">
		<strong><img title="夏日生活必备" src="http://files.zheshangonline.com/20100806/main_28.gif" /></strong>
		<ul class="nr1">
			<li>
				<a target="_blank" title="包邮 秒杀 九洲鹿 不锈钢 宫廷蚊帐JWZ061帐绳另售" href="http://taogou.zheshangonline.com/productshow-1078655477.html">
					<img alt="包邮 秒杀 九洲鹿 不锈钢 宫廷蚊帐JWZ061帐绳另售 " src="http://files.zheshangonline.com/20100806/main_31.gif" />
					<em>包邮 秒杀 九洲鹿 不锈钢 宫廷蚊帐JWZ061帐绳另售 </em>
				</a>
				<b>￥99</b>
			</li>
			<li>
				<a target="_blank" title="徐静蕾最爱 五折超强防紫 外线太阳伞遮阳伞" href="http://taogou.zheshangonline.com/productshow-2776313647.html">
					<img alt="徐静蕾最爱 五折超强防紫 外线太阳伞遮阳伞" src="http://files.zheshangonline.com/20100806/main_32.gif" />
					<em>徐静蕾最爱 五折超强防紫 外线太阳伞遮阳伞</em>
				</a>
				<b>￥45</b>
			</li>
			<li>
				<a target="_blank" title="雅高 正品沃泰多功能冰垫 坐垫/冰枕/凉垫/散热垫" href="http://taogou.zheshangonline.com/productshow-5264834724.html">
					<img alt="雅高 正品沃泰多功能冰垫 坐垫/冰枕/凉垫/散热垫" src="http://files.zheshangonline.com/20100806/main_34.gif" />
					<em>雅高 正品沃泰多功能冰垫 坐垫/冰枕/凉垫/散热垫</em>
				</a>
				<b>￥34.9</b>
			</li>
			<li>
				<a target="_blank" title="四皇冠包邮【帝娜】二开 门蒙古包蚊帐" href="http://taogou.zheshangonline.com/productshow-105685950.html">
					<img alt="四皇冠包邮【帝娜】二开 门蒙古包蚊帐" src="http://files.zheshangonline.com/20100806/main_36.gif" />
					<em>四皇冠包邮【帝娜】二开 门蒙古包蚊帐</em>
				</a>
				<b>￥33.9</b>
			</li>
			<li>
				<a target="_blank" title="专供多乐眯乳胶枕护颈椎保 健枕/枕头 限量1000个" href="http://taogou.zheshangonline.com/productshow-2222641369.html">
					<img alt="专供多乐眯乳胶枕护颈椎保 健枕/枕头 限量1000个" src="http://files.zheshangonline.com/20100806/main_37.gif" />
					<em>专供多乐眯乳胶枕护颈椎保 健枕/枕头 限量1000个</em>
				</a>
				<b>￥99</b>
			</li>
		</ul>
	</div>
	<div class="con bg1" id="prd3">
		<strong><img title="夏日美装必备" src="http://files.zheshangonline.com/20100806/main_13.gif" /></strong>
		<ul class="nr1">
			<li>
				<a target="_blank" title="韩版 巴厘岛苏菲海洋长裙 波米沙滩裙 露背裙 海边" href="http://taogou.zheshangonline.com/productshow-5364962284.html">
					<img alt="韩版 巴厘岛苏菲海洋长裙 波米沙滩裙 露背裙 海边" src="http://files.zheshangonline.com/20100806/main_47.gif" />
					<em>韩版 巴厘岛苏菲海洋长裙 波米沙滩裙 露背裙 海边</em>
				</a>
				<b>￥118</b>
			</li>
			<li>
				<a target="_blank" title="日本超人气 韩国超人气 比基 尼钢托泳衣 送超大百搭披纱" href="http://taogou.zheshangonline.com/productshow-3545399391.html">
					<img alt="日本超人气 韩国超人气 比基 尼钢托泳衣 送超大百搭披纱" src="http://files.zheshangonline.com/20100806/main_49.gif" />
					<em>日本超人气 韩国超人气 比基 尼钢托泳衣 送超大百搭披纱</em>
				</a>
				<b>￥48</b>
			</li>
			<li>
				<a target="_blank" title="2010新款泳装 韩版可爱 比基尼 专柜游泳衣" href="http://taogou.zheshangonline.com/productshow-2805376625.html">
					<img alt="2010新款泳装 韩版可爱 比基尼 专柜游泳衣" src="http://files.zheshangonline.com/20100806/main_51.gif" />
					<em>2010新款泳装 韩版可爱 比基尼 专柜游泳衣</em>
				</a>
				<b>￥38</b>
			</li>
			<li>
				<a target="_blank" title="波西米亚长裙 吊带大摆连衣 裙 雪纺连衣裙子" href="http://taogou.zheshangonline.com/productshow-4756390490.html">
					<img alt="波西米亚长裙 吊带大摆连衣 裙 雪纺连衣裙子" src="http://files.zheshangonline.com/20100806/main_53.gif" />
					<em>波西米亚长裙 吊带大摆连衣 裙 雪纺连衣裙子</em>
				</a>
				<b>￥38</b>
			</li>
			<li>
				<a target="_blank" title="韩版修身显瘦波西米亚长裙 花苞半身裙8802" href="http://taogou.zheshangonline.com/productshow-4388275163.html">
					<img alt="韩版修身显瘦波西米亚长裙 花苞半身裙8802" src="http://files.zheshangonline.com/20100806/main_54.gif" />
					<em>韩版修身显瘦波西米亚长裙 花苞半身裙8802</em>
				</a>
				<b>￥108</b>
			</li>
		</ul>
	</div>
	<div class="con bg2" id="prd4">
		<strong><img title="夏日配饰必备" src="http://files.zheshangonline.com/20100806/main_61.gif" /></strong>
		<ul class="nr1">
			<li>
				<a target="_blank" title="【涩谷】可爱波点大蝴蝶结 粗绳小碎花脚垫鱼嘴凉鞋" href="http://taogou.zheshangonline.com/productshow-5005066306.html">
					<img alt="【涩谷】可爱波点大蝴蝶结 粗绳小碎花脚垫鱼嘴凉鞋" src="http://files.zheshangonline.com/20100806/main_64.gif" />
					<em>【涩谷】可爱波点大蝴蝶结 粗绳小碎花脚垫鱼嘴凉鞋</em>
				</a>
				<b>￥128</b>
			</li>
			<li>
				<a target="_blank" title="Dopie拖鞋最潮人字拖-销量 破百万情侣鞋拖鞋" href="http://taogou.zheshangonline.com/productshow-5062020317.html">
					<img alt="Dopie拖鞋最潮人字拖-销量 破百万情侣鞋拖鞋" src="http://files.zheshangonline.com/20100806/main_66.gif" />
					<em>Dopie拖鞋最潮人字拖-销量 破百万情侣鞋拖鞋</em>
				</a>
				<b>￥70</b>
			</li>
			<li>
				<a target="_blank" title="日单专利夏天防紫外线防水 太阳帽 大沿帽子 沙滩帽" href="http://taogou.zheshangonline.com/productshow-1506531866.html">
					<img alt="日单专利夏天防紫外线防水 太阳帽 大沿帽子 沙滩帽" src="http://files.zheshangonline.com/20100806/main_68.gif" />
					<em>日单专利夏天防紫外线防水 太阳帽 大沿帽子 沙滩帽</em>
				</a>
				<b>￥48</b>
			</li>
			<li>
				<a target="_blank" title="浪美〓98分贝2010新牛皮精 致简约真皮单肩包/女包包" href="http://taogou.zheshangonline.com/productshow-5816147920.html">
					<img alt="浪美〓98分贝2010新牛皮精 致简约真皮单肩包/女包包" src="http://files.zheshangonline.com/20100806/main_70.gif" />
					<em>浪美〓98分贝2010新牛皮精 致简约真皮单肩包/女包包</em>
				</a>
				<b>￥98</b>
			</li>
			<li>
				<a target="_blank" title="GUFEL酷妃欧美新款花朵罗马 真皮女士高跟防水台女凉鞋" href="http://taogou.zheshangonline.com/productshow-4531243913.html">
					<img alt="GUFEL酷妃欧美新款花朵罗马 真皮女士高跟防水台女凉鞋" src="http://files.zheshangonline.com/20100806/main_72.gif" />
					<em>GUFEL酷妃欧美新款花朵罗马 真皮女士高跟防水台女凉鞋</em>
				</a>
				<b>￥196</b>
			</li>
		</ul>
	</div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

