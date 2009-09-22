<%@ Page Language="C#" Inherits="SAS.ManageWeb.ManagePage.syslogin"  EnableViewstate ="false" Codebehind="syslogin.aspx.cs"%>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html dir="ltr" xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="X-UA-Compatible" content="IE=7" />
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>登录</title>
<link href="style/comm.css" type="text/css" rel="stylesheet" />
<link href="style/admin.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
    if (top.location != self.location) {
        top.location.href = "syslogin.aspx";
    }
		</script>
</head>

<body>

<form id="form1" runat="server" method="post">

<div class="adlog">
	<p class="adlog1 adbg"></p>
	<div class="adlog2" style="height:30px; margin-top:50px;">
		<asp:literal id="Msg" runat="server"></asp:literal>
	</div>
	<div class="adlog2">
		<p class="adlog2lt"><img src="images/logo1.png" alt="" title="" />
		<p class="adlog2ce adbg"></p>
		<div class="adlog2rt">
			<p class="adlrt1">
				<span class="adlrt1lt">用户名：</span>
				<span class="adlrt1rt"><cc1:textbox id="UserName" runat="server" RequiredFieldType="暂无校验" Text="" size="20" ></cc1:textbox></span>
			</p>
			<p class="adlrt1">
				<span class="adlrt1lt">密　码：</span>
				<span class="adlrt1rt"><cc1:textbox id="PassWord" runat="server" RequiredFieldType="暂无校验" Text="" TextMode="Password" size="20"  ></cc1:textbox></span>
			</p>
			<p class="adlrt1">
				<span class="adlrt1lt">验证码：</span>
				<span class="adlrt1rt">
				<em class="adlrt2">
				<input id="vcode" onkeydown="if(event.keyCode==13)  document.getElementById('login').focus();" type="text" size="20" name="vcode" autocomplete="off" class="input1" style="width:55px;" />
				</em>
				<em class="adlrt2">
				<img id="vcodeimg" style="cursor:hand" onclick="this.src='../tools/VerifyImagePage.aspx?time=' + Math.random()" title="点击刷新验证码" align="absMiddle" src="" alt="" />
				<script type="text/javascript">
                        document.getElementById('vcodeimg').src='../tools/VerifyImagePage.aspx?id=<%=olid.ToString()%>&time=' + Math.random();
                        document.getElementById('vcode').value = "";
					</script>
				</em>
				</span>
			</p>
			<p class="adlrt1">
				<span class="adlrt1lt"></span>
				<span class="adlrt1rt2"><input id="login" type="submit" class="an3" value="登 录" /></span>
			</p>
		</div>
	</div>
</div>
<div class="adbot"><%=footer%></div>
</form>
<div id="copyright"></div>
</body>

</html>
