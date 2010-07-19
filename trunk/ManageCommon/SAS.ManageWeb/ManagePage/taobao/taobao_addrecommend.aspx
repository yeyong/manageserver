<%@ Page Language="C#" CodeBehind="taobao_addrecommend.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.taobao_addrecommend" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta name="keywords" content="天狼星,工作室" />
    <meta name="description" content="天狼星工作室综合管理后台" />
    <title>天狼星工作室综合管理后台-推荐添加</title>
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="ManagerForm">
		<fieldset>
		<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">添加推荐组</legend>
		<table class="table1" cellspacing="0" cellpadding="8" width="100%" align="center" >
		    <tr align="center">
		        <td><button type="button" class="ManagerButton" onclick="window.location='taobao_additemrecommend.aspx';"><img src="../images/submit.gif" /> 添加商品推荐组</button></td>
		        <td><button type="button" class="ManagerButton" onclick="window.location='taobao_addshoprecommend.aspx';"><img src="../images/submit.gif" /> 添加店铺推荐组</button></td>
		        <td><button type="button" class="ManagerButton" onclick="window.location='taobao_recommendgrid.aspx?ctype=3';"><img src="../images/submit.gif" /> 添加活动推荐组</button></td>
                <td><button type="button" class="ManagerButton" onclick="window.location='taobao_recommendgrid.aspx?ctype=4';"><img src="../images/submit.gif" /> 添加频道推荐组</button></td>
		    </tr>
		</table>
		</fieldset>
	</div>
    </form>
    <%=footer%>
</body>
</html>
