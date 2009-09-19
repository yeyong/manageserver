<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Page Language="C#" %>
<html dir="ltr" xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>登录</title>
<link href="style/comm.css" type="text/css" rel="stylesheet" />
<link href="style/admin.css" type="text/css" rel="stylesheet" />
</head>

<body>

<form id="form1" runat="server">

<div class="adlog">
	<p class="adlog1 adbg"></p>
	<div class="adlog2" style="height:30px; margin-top:50px;">
		<p class="adlrt1 zi1" style=" float:right; letter-spacing:1px; display:none;"><span class="adlrt1tu adbg"></span>验证码错误,请重新输入</p>
	</div>
	<div class="adlog2">
		<p class="adlog2lt"><img src="images/logo1.png" alt="" title="" /></p>
		<p class="adlog2ce adbg"></p>
		<div class="adlog2rt">
			<p class="adlrt1">
				<span class="adlrt1lt">用户名：</span>
				<span class="adlrt1rt"><input type="text" name="textfield" class="input1" style="width:120px;" /></span>
			</p>
			<p class="adlrt1">
				<span class="adlrt1lt">密　码：</span>
				<span class="adlrt1rt"><input type="text" name="textfield" class="input1" style="width:120px;" /></span>
			</p>
			<p class="adlrt1">
				<span class="adlrt1lt">验证码：</span>
				<span class="adlrt1rt">
				<em class="adlrt2">
				<input type="text" name="textfield" class="input1" style="width:55px;" />
				</em>
				<em class="adlrt2">
				<img alt="" src="images/ad/ad-2.png" />
				</em>
				</span>
			</p>
			<p class="adlrt1">
				<span class="adlrt1lt"></span>
				<span class="adlrt1rt2"><input type="button" name="button1" class="an3" value="登 录" /></span>
			</p>
		</div>
	</div>
</div>
<div class="adbot"></div>
</form>

</body>

</html>
