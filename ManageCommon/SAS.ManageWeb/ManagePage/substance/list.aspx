<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Page Language="C#" %>
<html dir="ltr" xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="X-UA-Compatible" content="IE=7" />
<meta name="description" content="网站简介" />
<meta name="keywords" content="" />
<title>列表</title>
<link href="../style/comm.css" type="text/css" rel="stylesheet" />
<link href="../style/admin.css" type="text/css" rel="stylesheet" />
<script src="../scripts/jquery.js" type="text/javascript"></script>
</head>

<body>

<form id="form1" runat="server">

<div class="xinrt">
	<div class="pertit">
		<p class="pertit1">
		下单单号：
		<input type="text" name="textfield" class="input1" style="width:130px;" />
		　用户名：
		<input type="text" name="textfield" class="input1" style="width:130px;" />
		　购物人：
		<input type="text" name="textfield" class="input1" style="width:130px;" />
		　收货人：
		<input type="text" name="textfield" class="input1" style="width:130px;" />　　
		<input type="button" name="button2" class="an1" value="查询" />
		</p>
		<p class="pertit1">
		　　邮编：
		<input type="text" name="textfield" class="input1" style="width:130px;" />
		手机号码：
		<input type="text" name="textfield" class="input1" style="width:130px;" />
		联系电话：
		<input type="text" name="textfield" class="input1" style="width:130px;" />
		电子邮件：
		<input type="text" name="textfield" class="input1" style="width:130px;" />
		</p>
		<p class="pertit1">
		下单时间：
		<input type="text" name="textfield" class="input1" style="width:130px;" />
		　　至　　
		<input type="text" name="textfield" class="input1" style="width:130px;" />
		　用户名：
		<input type="text" name="textfield" class="input1" style="width:130px;" />
		订单状态：
		<select name="select">
			<option>已付</option>
			<option>未付</option>
		</select>
		</p>
		<p class="pertit2">
			<span class="pertnr1"><a href="#" class="zt2">精简显示</a></span>
		</p>
	</div>
	<div class="rtcot">
		<ul class="nictit">
			<li>
				<a href="#" class="zt1" style="cursor:pointer;">
				<p class="nictittu" style=" background:url(../images/admin-gif/access-127.gif) no-repeat;"></p>
				<p class="nictitzt">添加</p>
				</a>
			</li>
			<li>
				<a href="#" class="zt1" style="cursor:pointer;">
				<p class="nictittu adbg" style=" background:url(../images/admin-gif/access-128.gif) no-repeat;"></p>
				<p class="nictitzt">删除</p>
				</a>
			</li>
			<li>
				<a href="#" class="zt1" style="cursor:pointer;">
				<p class="nictittu adbg" style=" background:url(../images/admin-gif/access-134.gif) no-repeat;"></p>
				<p class="nictitzt">回收站</p>
				</a>
			</li>
			<li>
				<a href="#" class="zt1" style="cursor:pointer;">
				<p class="nictittu adbg" style=" background:url(../images/admin-gif/access-15.gif) no-repeat;"></p>
				<p class="nictitzt">导入</p>
				</a>
			</li>
			<li>
				<a href="#" class="zt1" style="cursor:pointer;">
				<p class="nictittu adbg" style=" background:url(../images/admin-gif/access-16.gif) no-repeat;"></p>
				<p class="nictitzt">导出</p>
				</a>
			</li>
			<li>
				<a href="#" class="zt1" style="cursor:pointer;">
				<p class="nictittu adbg" style=" background:url(../images/admin-gif/access-145.gif) no-repeat;"></p>
				<p class="nictitzt">向上</p>
				</a>
			</li>
			<li>
				<a href="#" class="zt1" style="cursor:pointer;">
				<p class="nictittu adbg" style=" background:url(../images/admin-gif/access-50.gif) no-repeat;"></p>
				<p class="nictitzt">向下</p>
				</a>
			</li>
		</ul>
		
		<table width="100%" border="0" cellspacing="0" cellpadding="0" class="adtb">
		<thead>
		<tr>
			<td style="width:5%;"><input type="checkbox" name="checkbox" value="checkbox" /></td>
			<td style="width:10%;">商品编号</td>
			<td style="width:10%;">商品名称</td>
			<td style="width:10%;">图片</td>
			<td style="width:10%;">销售价</td>
			<td style="width:10%;">库存</td>
			<td style="width:10%;">上架</td>
			<td style="width:10%;">品牌</td>
			<td style="width:10%;">重量</td>
			<td style="width:15%;">操作</td>
		</tr>
		</thead>
		<tbody>
		<tr>
			<td><input type="checkbox" name="checkbox" value="checkbox" /></td>
			<td><input type="text" value="00000000" class="ginput1" /></td>
			<td><input type="text" value="黑色迷你T恤　帅气" class="ginput1" /></td>
			<td><img alt="" src="../images/admin-gif/access-146.gif" /></td>
			<td><input type="text" value="￥58" class="ginput1" /></td>
			<td><input type="text" value="100" class="ginput1" /></td>
			<td>已收架</td>
			<td><input type="text" value="美邦" class="ginput1" /></td>
			<td><input type="text" value="20G" class="ginput1" /></td>
			<td>
			<a href="edit.aspx" title=""><img alt="" src="../images/admin-gif/access-9.gif" /></a>　
			<a href="#" title=""><img alt="" src="../images/admin-gif/access-129.gif" /></a>
			</td>
		</tr>
		<tr>
			<td><input type="checkbox" name="checkbox" value="checkbox" /></td>
			<td><input type="text" value="00000000" class="ginput1" /></td>
			<td><input type="text" value="黑色迷你T恤　帅气" class="ginput1" /></td>
			<td><img alt="" src="../images/admin-gif/access-146.gif" /></td>
			<td><input type="text" value="￥58" class="ginput1" /></td>
			<td><input type="text" value="100" class="ginput1" /></td>
			<td>已收架</td>
			<td><input type="text" value="美邦" class="ginput1" /></td>
			<td><input type="text" value="20G" class="ginput1" /></td>
			<td>
			<a href="edit.aspx" title=""><img alt="" src="../images/admin-gif/access-9.gif" /></a>
			</td>
		</tr>
		<tr>
			<td><input type="checkbox" name="checkbox" value="checkbox" /></td>
			<td><input type="text" value="00000000" class="ginput1" /></td>
			<td><input type="text" value="黑色迷你T恤　帅气" class="ginput1" /></td>
			<td><img alt="" src="../images/admin-gif/access-146.gif" /></td>
			<td><input type="text" value="￥58" class="ginput1" /></td>
			<td><input type="text" value="100" class="ginput1" /></td>
			<td>已收架</td>
			<td><input type="text" value="美邦" class="ginput1" /></td>
			<td><input type="text" value="20G" class="ginput1" /></td>
			<td>
			<a href="edit.aspx" title=""><img alt="" src="../images/admin-gif/access-9.gif" /></a>
			</td>
		</tr>
		<tr>
			<td><input type="checkbox" name="checkbox" value="checkbox" /></td>
			<td><input type="text" value="00000000" class="ginput1" /></td>
			<td><input type="text" value="黑色迷你T恤　帅气" class="ginput1" /></td>
			<td><img alt="" src="../images/admin-gif/access-146.gif" /></td>
			<td><input type="text" value="￥58" class="ginput1" /></td>
			<td><input type="text" value="100" class="ginput1" /></td>
			<td>已收架</td>
			<td><input type="text" value="美邦" class="ginput1" /></td>
			<td><input type="text" value="20G" class="ginput1" /></td>
			<td>
			<a href="edit.aspx" title=""><img alt="" src="../images/admin-gif/access-9.gif" /></a>
			</td>
		</tr>
		<tr>
			<td><input type="checkbox" name="checkbox" value="checkbox" /></td>
			<td><input type="text" value="00000000" class="ginput1" /></td>
			<td><input type="text" value="黑色迷你T恤　帅气" class="ginput1" /></td>
			<td><img alt="" src="../images/admin-gif/access-146.gif" /></td>
			<td><input type="text" value="￥58" class="ginput1" /></td>
			<td><input type="text" value="100" class="ginput1" /></td>
			<td>已收架</td>
			<td><input type="text" value="美邦" class="ginput1" /></td>
			<td><input type="text" value="20G" class="ginput1" /></td>
			<td>
			<a href="edit.aspx" title=""><img alt="" src="../images/admin-gif/access-9.gif" /></a>
			</td>
		</tr>
		<tr>
			<td><input type="checkbox" name="checkbox" value="checkbox" /></td>
			<td><input type="text" value="00000000" class="ginput1" /></td>
			<td><input type="text" value="黑色迷你T恤　帅气" class="ginput1" /></td>
			<td><img alt="" src="../images/admin-gif/access-146.gif" /></td>
			<td><input type="text" value="￥58" class="ginput1" /></td>
			<td><input type="text" value="100" class="ginput1" /></td>
			<td>已收架</td>
			<td><input type="text" value="美邦" class="ginput1" /></td>
			<td><input type="text" value="20G" class="ginput1" /></td>
			<td>
			<a href="edit.aspx" title=""><img alt="" src="../images/admin-gif/access-9.gif" /></a>
			</td>
		</tr>
		<tr>
			<td><input type="checkbox" name="checkbox" value="checkbox" /></td>
			<td><input type="text" value="00000000" class="ginput1" /></td>
			<td><input type="text" value="黑色迷你T恤　帅气" class="ginput1" /></td>
			<td><img alt="" src="../images/admin-gif/access-146.gif" /></td>
			<td><input type="text" value="￥58" class="ginput1" /></td>
			<td><input type="text" value="100" class="ginput1" /></td>
			<td>已收架</td>
			<td><input type="text" value="美邦" class="ginput1" /></td>
			<td><input type="text" value="20G" class="ginput1" /></td>
			<td>
			<a href="edit.aspx" title=""><img alt="" src="../images/admin-gif/access-9.gif" /></a>
			</td>
		</tr>
		<tr>
			<td><input type="checkbox" name="checkbox" value="checkbox" /></td>
			<td><input type="text" value="00000000" class="ginput1" /></td>
			<td><input type="text" value="黑色迷你T恤　帅气" class="ginput1" /></td>
			<td><img alt="" src="../images/admin-gif/access-146.gif" /></td>
			<td><input type="text" value="￥58" class="ginput1" /></td>
			<td><input type="text" value="100" class="ginput1" /></td>
			<td>已收架</td>
			<td><input type="text" value="美邦" class="ginput1" /></td>
			<td><input type="text" value="20G" class="ginput1" /></td>
			<td>
			<a href="edit.aspx" title=""><img alt="" src="../images/admin-gif/access-9.gif" /></a>
			</td>
		</tr>
		<tr>
			<td><input type="checkbox" name="checkbox" value="checkbox" /></td>
			<td><input type="text" value="00000000" class="ginput1" /></td>
			<td><input type="text" value="黑色迷你T恤　帅气" class="ginput1" /></td>
			<td><img alt="" src="../images/admin-gif/access-146.gif" /></td>
			<td><input type="text" value="￥58" class="ginput1" /></td>
			<td><input type="text" value="100" class="ginput1" /></td>
			<td>已收架</td>
			<td><input type="text" value="美邦" class="ginput1" /></td>
			<td><input type="text" value="20G" class="ginput1" /></td>
			<td>
			<a href="edit.aspx" title=""><img alt="" src="../images/admin-gif/access-9.gif" /></a>
			</td>
		</tr>
		<tr>
			<td><input type="checkbox" name="checkbox" value="checkbox" /></td>
			<td><input type="text" value="00000000" class="ginput1" /></td>
			<td><input type="text" value="黑色迷你T恤　帅气" class="ginput1" /></td>
			<td><img alt="" src="../images/admin-gif/access-146.gif" /></td>
			<td><input type="text" value="￥58" class="ginput1" /></td>
			<td><input type="text" value="100" class="ginput1" /></td>
			<td>已收架</td>
			<td><input type="text" value="美邦" class="ginput1" /></td>
			<td><input type="text" value="20G" class="ginput1" /></td>
			<td>
			<a href="edit.aspx" title=""><img alt="" src="../images/admin-gif/access-9.gif" /></a>
			</td>
		</tr>
		</tbody>
		</table>
		<div class="page">
	共 7 页, 当前第 1 页   [ <a href="#" class="zt2">首页</a> | <a href="#" class="zt2">上一页</a> | <a href="#" class="zt2">下一页</a> | <a href="#" class="zt2">末页</a> ] 页数:
			<select name="select">
			<option>1</option>
			<option>2</option>
			</select>
		</div>
	</div>
	

</div>

</form>

</body>

</html>
